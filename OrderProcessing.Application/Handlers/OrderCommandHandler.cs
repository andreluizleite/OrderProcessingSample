using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Api.Middlewares;
using OrderProcessing.Application.Exceptions;
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
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public PlaceOrderCommandHandler(IOrderRepository orderRepository, RabbitMQProducer rabbitMQProducer, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _orderRepository = orderRepository;
            _rabbitMQProducer = rabbitMQProducer;
            _logger = logger;
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
            catch (BusinessLogicException ex)
            {
                _logger.LogWarning(ex, "Business logic error occurred: {Message}", ex.Message);
                throw;
            }
            catch (InfrastructureException ex)
            {
                _logger.LogError(ex, "Infrastructure error occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the order: {Message}", ex.Message);
                throw new InfrastructureException("An error occurred while processing the order.", ex);
            }
        }
    }
}
