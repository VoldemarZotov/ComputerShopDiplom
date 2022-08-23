using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Order
{
    class ShoppingCart
    {
        public List<ShoppingCartItem> OrderItems { get; set; }
        public decimal FinalCost { get; set; }

        public ShoppingCart()
        {
            FinalCost = 0;
            OrderItems = new List<ShoppingCartItem>();
        }

        public void AddItem(ShoppingCartItem item)
        {

            if (OrderItems.FirstOrDefault(x => x.Id == item.Id) == null)
            {
                OrderItems.Add(item);
                FinalCost += item.Cost;
            }
            else
            {
                if (OrderItems.FirstOrDefault(x => x.Id == item.Id).Count < item.MaxCount)
                {
                    OrderItems.FirstOrDefault(x => x.Id == item.Id).Count += 1;
                    FinalCost += item.Cost;
                }
            }
        }

        public void RemoveItem(ShoppingCartItem item)
        {

            if (OrderItems.FirstOrDefault(x => x.Id == item.Id).Count == 1)
            {
                OrderItems.Remove(item);
                FinalCost -= item.Cost;
            }
            else if (OrderItems.FirstOrDefault(x => x.Id == item.Id).Count > 1)
            {
                OrderItems.FirstOrDefault(x => x.Id == item.Id).Count -= 1;
                FinalCost -= item.Cost;
            }
        }
    }
}