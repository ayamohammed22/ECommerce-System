using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Quaries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpGet("GetBasketByUserName")]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket([FromQuery] GetBasketByUserNameQuery query)
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

        [Route("{action}")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get basket with username
            var basket = await _mediator.Send(new GetBasketByUserNameQuery { UserName = basketCheckout.UserName });
            if (basket == null)
            {
                return BadRequest();
            }
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            // remove basket
            await _mediator.Send(new DeleteBasketByUserNameCommand { UserName = basketCheckout.UserName });
            return Accepted();
        }
    }
}
