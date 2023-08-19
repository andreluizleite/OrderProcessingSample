using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using OrderProcessing.Domain.Repositories;
using OrderProcessing.Infrastructure.Repositories;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.Messaging;
using Prometheus;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.Metrics;

namespace OrderProcessing.Application
{
    public static class OrderProcessingModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<Counter>(provider =>
            {
                return Metrics.CreateCounter("myapp_requests_total", "Total number of requests");
            });


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PlaceOrderCommand).Assembly));

            // Register RabbitMQProducer
            services.AddSingleton<RabbitMQProducer>();

            // Register other application-specific dependencies
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            var counter = Metrics.CreateCounter("myapp_requests_total", "Total number of requests");
            app.Use(async (context, next) =>
            {
                // Increment the counter metric
                counter.Inc();

                // Continue processing the request
                await next.Invoke();
            });
        }

    }
}
