using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Const
{
    enum Role
    {
        [Display(Name = "Администратор")]
        ADMIN =1,

        [Display(Name = "Менеджер")]
        MANAGER =2,

        [Display(Name = "Продавец")]
        SELLER =3
    }
}
