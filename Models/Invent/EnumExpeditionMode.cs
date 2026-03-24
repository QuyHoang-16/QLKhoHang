using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum ExpeditionMode
    {
        [Display(Name = "Đường bộ")]
        Land = 1,

        [Display(Name = "Đường biển")]
        Sea = 2,

        [Display(Name = "Đường hàng không")]
        Air = 3
    }

}
