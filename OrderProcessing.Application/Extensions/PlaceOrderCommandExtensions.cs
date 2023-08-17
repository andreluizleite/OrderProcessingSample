using System.Linq;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.DTOs;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;

namespace OrderProcessing.Application.Extensions
{
    public static class PlaceOrderCommandExtensions
    {
        public static Order ToOrder(this PlaceOrderCommand command)
        {
            var order = new Order(command.CustomerId,
                command.Items.Select(
                    item => new OrderItem(new Product(item.ProductId),
                    item.Quantity,
                    item.Price)
                    ).ToList()
                );

            return order;
        }
    }
}
