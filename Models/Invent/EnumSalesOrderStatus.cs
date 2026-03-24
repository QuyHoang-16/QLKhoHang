using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum SalesOrderStatus
    {
        [Display(Name = "Nháp")]
        Draft = -1,

        [Display(Name = "Đang mở")]
        Open = 1,

        [Display(Name = "Hoàn tất")]
        Completed = 2
    }
}
