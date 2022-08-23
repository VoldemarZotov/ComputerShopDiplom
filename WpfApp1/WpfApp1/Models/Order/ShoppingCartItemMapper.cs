using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Order
{
    public static class ShoppingCartItemMapper
    {
        public static ShoppingCartItem MapToCartItem(this ItemsViewModel item)
        {

            return new ShoppingCartItem()
            {
                Id = item.Id,
                Name = item.Name,
                Cost = item.Cost,
                Count = 1,
                Type = item.Type,
                MaxCount = item.MaxCount
            };
        }
    }
}
