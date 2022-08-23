using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.ViewModel
{
    internal class MemoryViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }
        public int type { get; set; }
        public string typeName { get; set; }
        public int storage { get; set; }
        public string storageName { get; set; }
    }
}
