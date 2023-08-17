using MediatR;
using OrderProcessing.Application.Extensions;
using OrderProcessing.Application.Messaging; // Import the namespace for RabbitMQProducer
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Repositories;

namespace OrderProcessing.Application.Commands
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public PlaceOrderCommandHandler(IOrderRepository orderRepository, RabbitMQProducer rabbitMQProducer)
        {
            _orderRepository = orderRepository;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<OrderCommandResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate and process the order
                var order = request.ToOrder();

                await _orderRepository.AddAsync(order);

                // Publish an event to RabbitMQ
                _rabbitMQProducer.PublishOrderPlacedEvent(order.Id.ToString());

                return new OrderCommandResult { OrderId = order.Id };


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the order.", ex);
            }
        }
    }
}
