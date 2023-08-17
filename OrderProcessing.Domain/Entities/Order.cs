using System;
using System.Collections.Generic;

namespace OrderProcessing.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        private Order()
        {
            Id = Guid.NewGuid(); // Generate a new Guid for the Id
            Items = new List<OrderItem>();
        }

        public Order(Guid customerId, List<OrderItem> items)
        {
            Id = Guid.NewGuid(); // Generate a new Guid for the Order Id
            CustomerId = customerId;
            Items = items;

            throw new NotImplementedException();
        }
    }
}
