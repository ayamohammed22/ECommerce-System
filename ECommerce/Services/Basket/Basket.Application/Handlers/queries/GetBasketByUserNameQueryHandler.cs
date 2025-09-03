using AutoMapper;
using Basket.Application.Quaries;
using Basket.Application.Responses;
using Basket.Core.Repositaries;
using MediatR;

namespace Basket.Application.Handlers.queries
{
    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public GetBasketByUserNameQueryHandler(IBasketRepo basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepo.GetBasket(request.UserName);
            var mappedCart = _mapper.Map<ShoppingCartResponse>(shoppingCart);
            return mappedCart;
        }
    }
}
