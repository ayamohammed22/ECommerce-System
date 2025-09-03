using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Quaries
{
    public class GetBasketByUserNameQuery : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }
    }
}
