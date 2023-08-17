using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using OrderProcessing.Domain.Repositories;
using OrderProcessing.Infrastructure.Repositories;

namespace OrderProcessing.Application
{
    public static class OrderProcessingModule
    {
        public static void Configure(IServiceCollection services)
        {
            // Register other application-specific dependencies
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
