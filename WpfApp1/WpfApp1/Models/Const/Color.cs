using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Const
{
/**
 * Цвет корпуса
 */
    public enum ColorType
    {
        [Display(Name="Белый")]
        WHITE = 1,

        [Display(Name = "Черный")]
        BLACK = 2,

        [Display(Name = "Серый")]
        GRAY = 3
    }

}
