﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.ViewModel
{
    internal class EmployeeViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public string roleName { get; set; }
    }
}
