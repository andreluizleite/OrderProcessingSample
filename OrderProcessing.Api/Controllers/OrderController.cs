using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.Responses;
using OpenTracing;

namespace OrderProcessing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITracer _tracer;

        public OrderController(ITracer tracer, IMediator mediator)
        {
            _mediator = mediator;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            try
            {
                using (var scope = _tracer.BuildSpan("PlaceOrder").WithTag("customerId", command.CustomerId.ToString()).StartActive(true))
                {
                    // Simulate a slow database query (10-second delay)
                    await Task.Delay(TimeSpan.FromSeconds(10));

                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    ErrorMessage = "An error occurred while processing the request.",
                    Details = ex.Message
                };

                return BadRequest(errorResponse);
            }
        }
    }
}
