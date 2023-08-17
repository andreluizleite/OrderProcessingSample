using OrderProcessing.Application.DTOs;
using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Extensions
{
    public static class OrderItemExtensions
    {
        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ProductId = orderItem.Product.Id,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
        }
    }
}
