using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum TOP
    {
        [Display(Name = "Thanh toán sau 10 ngày (D10)")]
        D10 = 10,

        [Display(Name = "Thanh toán sau 20 ngày (D20)")]
        D20 = 20,

        [Display(Name = "Thanh toán sau 30 ngày (D30)")]
        D30 = 30,

        [Display(Name = "Thanh toán sau 60 ngày (D60)")]
        D60 = 60
    }

}
