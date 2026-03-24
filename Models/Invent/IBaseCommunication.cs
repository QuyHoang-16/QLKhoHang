using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public interface IBaseCommunication
    {
        [Display(Name = "Điện thoại di động")]
        [StringLength(20)]
        string mobilePhone { get; set; }

        [Display(Name = "Điện thoại cơ quan")]
        [StringLength(20)]
        string officePhone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20)]
        string fax { get; set; }

        [Display(Name = "Email cá nhân")]
        [StringLength(50)]
        string personalEmail { get; set; }

        [Display(Name = "Email công việc")]
        [StringLength(50)]
        string workEmail { get; set; }

    }
}
