using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1.Models.Const;

namespace WpfApp1.Models.Order
{
    public class ItemsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int MaxCount { get; set; }
        public BitmapImage ImageBitmap { get; set; }
        public ItemType Type { get; set; }
    }
}
