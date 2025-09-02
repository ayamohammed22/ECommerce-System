using Basket.Core.Entities;

namespace Basket.Core.Repositaries
{
    public interface IBasketRepo
    {
        Task<ShoppingCart> GetBasket(string username);
        Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
        Task DeleteBasket(string username);
    }
}
