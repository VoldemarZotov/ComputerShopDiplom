using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1.Models.Const;

namespace WpfApp1.Models.Order
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; } = 0;
        public int MaxCount { get; set; }
        public decimal Cost { get; set; }
        public ItemType Type { get; set; }
    }
}
