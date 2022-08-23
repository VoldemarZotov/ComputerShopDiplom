using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.ViewModel
{
    internal class CorpusViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }
        public int color { get; set; }
        public string colorName { get; set; }
        public int formfactor { get; set; }
        public string formfactorName { get; set; }
    }
}
