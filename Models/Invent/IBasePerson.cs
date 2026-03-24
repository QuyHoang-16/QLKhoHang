using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public interface IBasePerson
    {
        [Display(Name = "Tên")]
        [Required]
        [StringLength(20)]
        string firstName { get; set; }

        [Display(Name = "Họ")]
        [Required]
        [StringLength(20)]
        string lastName { get; set; }

        [Display(Name = "Tên đệm")]
        [StringLength(20)]
        string middleName { get; set; }

        [Display(Name = "Biệt danh")]
        [StringLength(20)]
        string nickName { get; set; }

        [Display(Name = "Giới tính")]
        Gender gender { get; set; }

        [Display(Name = "Xưng hô")]
        Salutation salutation { get; set; }
    }


}
