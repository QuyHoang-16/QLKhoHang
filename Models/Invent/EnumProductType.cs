using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public enum ProductType
    {
        [Display(Name = "Thực phẩm")]
        Food = 1,

        [Display(Name = "Điện tử")]
        Electronic = 2,

        [Display(Name = "Hàng tiêu dùng nhanh (FMCG)")]
        FMCG = 3,

        [Display(Name = "Phần mềm")]
        Software = 4,

        [Display(Name = "Thời trang")]
        Fashion = 5
    }

}
