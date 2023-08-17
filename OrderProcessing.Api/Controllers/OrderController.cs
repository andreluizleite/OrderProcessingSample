using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.Responses;

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
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
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
