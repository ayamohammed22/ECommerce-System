using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile :Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponse>();
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
            CreateMap<CreateShoppingCartCommand, ShoppingCart>();
        }
    }
}
