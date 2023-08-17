using MediatR;
using System;
using System.Collections.Generic;

namespace OrderProcessing.Application.Commands
{
    public class PlaceOrderCommand : IRequest<OrderCommandResult>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemRequest> Items { get; set; }
    }

    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
