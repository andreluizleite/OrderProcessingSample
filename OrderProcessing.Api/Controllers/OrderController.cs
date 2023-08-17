using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrderProcessing.Application.Commands;

namespace OrderProcessing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
