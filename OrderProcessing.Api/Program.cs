using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using OrderProcessing.Api.Middlewares;
using OrderProcessing.Application;
using OrderProcessing.Infrastructure;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
builder.Services.AddDbContext<OrderProcessingDbContext>(
    options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OrderConnection")), ServiceLifetime.Transient);

// Register MediatR and handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSingleton<ITracer>(sp =>
{
    var serviceName = builder.Configuration["Jaeger:ServiceName"];
    var collectorHost = builder.Configuration["Jaeger:CollectorHost"];
    var collectorPort = int.Parse(builder.Configuration["Jaeger:CollectorPort"]);

    var tracer = new Tracer.Builder(serviceName)
        .WithReporter(new RemoteReporter.Builder()
            .WithSender(new UdpSender(collectorHost, collectorPort, 0))
            .Build())
        .WithSampler(new ConstSampler(true))
        .Build();

    return tracer;
});

OrderProcessingModule.Configure(builder.Services);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
