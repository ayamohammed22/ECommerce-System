using Basket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Responses
{
    public class ShoppingCartResponse 
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in ShoppingCartItems)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }

    }
}
