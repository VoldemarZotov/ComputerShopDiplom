using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Order
{
    internal class OrderItemViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
        public string CostOne { get; set; }
        public string CostFull { get; set; }
    }
}
