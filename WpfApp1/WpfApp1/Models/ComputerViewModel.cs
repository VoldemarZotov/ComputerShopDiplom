using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1.Models
{
    public class ComputerViewModel
    {

        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public decimal price { get; set; }
        public Nullable<bool> instock { get; set; }
        public BitmapImage  photo { get; set; }
        public byte[] photoByte { get; set; }

        public string id_processor { get; set; }
        public string id_mother { get; set; }
        public string id_videoadapter { get; set; }
        public string id_power { get; set; }
        public string id_hard_drive { get; set; }
        public string id_ram { get; set; }
        public string id_corpus { get; set; }
        public string id_employee { get; set; }

        public virtual corpus corpus { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual hard_drive hard_drive { get; set; }
        public virtual mother mother { get; set; }
        public virtual power power { get; set; }
        public virtual processor processor { get; set; }
        public virtual ram ram { get; set; }
        public virtual videoadapter videoadapter { get; set; }
    }
}
