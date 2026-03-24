using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum Gender
    {
        [Display(Name = "Nam")]
        Male = 1,

        [Display(Name = "Nữ")]
        Female = 2
    }

}
