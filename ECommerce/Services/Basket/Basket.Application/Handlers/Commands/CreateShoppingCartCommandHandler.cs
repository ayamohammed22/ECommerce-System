using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositaries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers.Commands
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IBasketRepo basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // will integrate with discount services
            var cart = _mapper.Map<ShoppingCart>(request);
            var shoppingCart = await _basketRepo.UpdateBasket(cart);
            var mappedCart = _mapper.Map<ShoppingCartResponse>(shoppingCart);
            return mappedCart;
        }
    }
}
