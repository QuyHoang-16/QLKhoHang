using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum BusinessSize
    {
        [Display(Name = "Doanh nghiệp nhỏ")]
        Small = 1,

        [Display(Name = "Doanh nghiệp vừa")]
        Medium = 2,

        [Display(Name = "Doanh nghiệp lớn")]
        Enterprise = 4,


    }

}
