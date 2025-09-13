using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GRPCServices;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositaries;
using MediatR;

namespace Basket.Application.Handlers.Commands
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;
        private readonly DiscountGRPCServices _discountServices;

        public CreateShoppingCartCommandHandler(IBasketRepo basketRepo, IMapper mapper, DiscountGRPCServices discountServices)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
            _discountServices = discountServices;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // will integrate with discount services
            foreach (var item in request.ShoppingCartItems)
            {
                var coupon = await _discountServices.GetDiscount(item.ProductName);
                if (coupon != null)
                {
                    item.Price -= coupon.Amount;
                }

            }
            var cart = _mapper.Map<ShoppingCart>(request);
            var shoppingCart = await _basketRepo.UpdateBasket(cart);
            var mappedCart = _mapper.Map<ShoppingCartResponse>(shoppingCart);
            return mappedCart;
        }
    }
}
