using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum ExpeditionType
    {
        [Display(Name = "Nội bộ")]
        Internal = 1,

        [Display(Name = "Thuê ngoài")]
        External = 2
    }

}
