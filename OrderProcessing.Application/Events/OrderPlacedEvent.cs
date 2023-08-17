using MediatR;
using System;

namespace OrderProcessing.Application.Events
{
    public class OrderPlacedEvent : INotification
    {
        public Guid OrderId { get; }
        public DateTime EventOccurredAt { get; }

        public OrderPlacedEvent(Guid orderId)
        {
            OrderId = orderId;
            EventOccurredAt = DateTime.UtcNow;
        }
    }
}
