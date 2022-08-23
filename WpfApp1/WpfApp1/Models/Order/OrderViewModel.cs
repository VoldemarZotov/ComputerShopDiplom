using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Order
{
    internal class OrderViewModel
    {
        public string Id { get; set; }
        public string ClientData { get; set; }
        public string Employee { get; set; }
        public string Date { get; set; }
        public string Cost { get; set; }
        public string Status { get; set; }
    }
}
