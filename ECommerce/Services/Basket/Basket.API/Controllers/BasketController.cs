using Basket.Application.Commands;
using Basket.Application.Quaries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
   
    public class BasketController : BaseController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetBasketByUserName")]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket([FromQuery]GetBasketByUserNameQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("CreateORUpdateBasket")]
        public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteBasket")]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket([FromQuery] DeleteBasketByUserNameCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
