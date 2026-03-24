using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public interface IBaseAddress
    {
        [Display(Name = "Địa chỉ (Số nhà / Đường 1)")]
        [Required]
        [StringLength(50)]
        string street1 { get; set; }

        [Display(Name = "Địa chỉ bổ sung (Đường 2)")]
        [StringLength(50)]
        string street2 { get; set; }

        [Display(Name = "Thành phố")]
        [StringLength(30)]
        string city { get; set; }

        [Display(Name = "Tỉnh / Thành phố")]
        [StringLength(30)]
        string province { get; set; }

        [Display(Name = "Quốc gia")]
        [StringLength(30)]
        string country { get; set; }
    }
}
