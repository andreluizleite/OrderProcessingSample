using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderProcessing.Domain.Repositories;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Application.Events; // Import the namespace for events
using OrderProcessing.Application.Extensions;

namespace OrderProcessing.Application.Commands
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator; // Inject IMediator

        public PlaceOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<OrderCommandResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            // Validate and process the order
            var order = request.ToOrder();

            await _orderRepository.AddAsync(order);

            // Publish an event
            var orderPlacedEvent = new OrderPlacedEvent(order.Id);
            await _mediator.Publish(orderPlacedEvent); // Publish the event

            return new OrderCommandResult { OrderId = order.Id };
        }
    }
}
