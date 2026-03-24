using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum UOM
    {
        [Display(Name = "Cái")]
        Pcs = 1,

        [Display(Name = "Chiếc")]
        EA = 2,

        [Display(Name = "Kilogram (Kg)")]
        Kg = 3,

        [Display(Name = "Lít")]
        Liter = 4,

        [Display(Name = "Hộp")]
        Box = 5,

        [Display(Name = "Thùng/phuy")]
        Drum = 6,

        [Display(Name = "Đơn vị")]
        Unit = 7
    }

}
