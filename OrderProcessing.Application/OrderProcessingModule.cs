using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using OrderProcessing.Domain.Repositories;
using OrderProcessing.Infrastructure.Repositories;
using OrderProcessing.Infrastructure;
using System.Configuration;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.Messaging;

namespace OrderProcessing.Application
{
    public static class OrderProcessingModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PlaceOrderCommand).Assembly));

            // Register RabbitMQProducer
            services.AddSingleton<RabbitMQProducer>();

            // Register other application-specific dependencies
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
