using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class VendorLine : INetcoreBasic, IBasePerson, IBaseCommunication
    {
        public VendorLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã liên hệ nhà cung cấp")]
        public string vendorLineId { get; set; }

        [StringLength(20)]
        [Display(Name = "Chức vụ")]
        [Required]
        public string jobTitle { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã nhà cung cấp")]
        public string vendorId { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public Vendor? vendor { get; set; }

        //IBasePerson
        [Display(Name = "Tên")]
        [Required]
        [StringLength(20)]
        public string firstName { get; set; }

        [Display(Name = "Họ")]
        [Required]
        [StringLength(20)]
        public string lastName { get; set; }

        [Display(Name = "Tên đệm")]
        [StringLength(20)]
        public string middleName { get; set; }

        [Display(Name = "Biệt danh")]
        [StringLength(20)]
        public string nickName { get; set; }

        [Display(Name = "Giới tính")]
        public Gender gender { get; set; }

        [Display(Name = "Xưng hô")]
        public Salutation salutation { get; set; }
        //IBasePerson

        //IBaseCommunication
        [Display(Name = "Điện thoại di động")]
        [StringLength(20)]
        public string mobilePhone { get; set; }

        [Display(Name = "Điện thoại cơ quan")]
        [StringLength(20)]
        public string officePhone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20)]
        public string fax { get; set; }

        [Display(Name = "Email cá nhân")]
        [StringLength(50)]
        public string personalEmail { get; set; }

        [Display(Name = "Email công việc")]
        [StringLength(50)]
        public string workEmail { get; set; }
        //IBaseCommunication

    }
}
