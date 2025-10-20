using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;

namespace Ordering.API.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetOrderByUserName")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrderByUserName([FromQuery] GetOrderListQuery query)
        {
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost("CheckoutOrder")]
        public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeletetOrder")]
        public async Task<IActionResult> DeleteOrder([FromQuery] DeleteOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckoutOrder([FromBody] UpdateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return NoContent();
        }
    }
}
