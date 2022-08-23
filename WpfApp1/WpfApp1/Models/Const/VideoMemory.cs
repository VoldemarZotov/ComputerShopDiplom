using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Const
{
    public enum VideoMemory
    {
        [Display(Name = "2 gb")]
        MEMORY2GB = 1,

        [Display(Name = "4 gb")]
        MEMORY4GB,

        [Display(Name = "6 gb")]
        MEMORY6GB,

        [Display(Name = "8 gb")]
        MEMORY8GB,

        [Display(Name = "12 gb")]
        MEMORY12GB,

        [Display(Name = "24 gb")]
        MEMORY24GB
    }
}
