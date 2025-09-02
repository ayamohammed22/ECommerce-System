using Basket.Core.Entities;
using Basket.Core.Repositaries;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositaries
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepo(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
          var basket = await _redisCache.GetStringAsync(username);
            if (String.IsNullOrEmpty(basket)) return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            var cart = await _redisCache.GetStringAsync(shoppingCart.UserName);
            if (cart is not null)
                await DeleteBasket(shoppingCart.UserName);
           await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.UserName);
        }

        public async Task DeleteBasket(string username)
        {
            var basket = _redisCache.GetStringAsync(username);  
            if(basket is not null)
                  await _redisCache.RemoveAsync(username);
        }
    }
}
