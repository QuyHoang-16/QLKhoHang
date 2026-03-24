using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class Warehouse: INetcoreBasic, IBaseAddress
    {
        public Warehouse()
        {
            this.createdAt = DateTime.UtcNow;
            this.warehouseId = Guid.NewGuid().ToString();
        }

        [StringLength(38)]
        [Display(Name = "Mã kho hàng")]
        public string warehouseId { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên kho hàng")]
        [Required]
        public string warehouseName { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả kho hàng")]
        public string description { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã chi nhánh")]
        [Required]
        public string branchId { get; set; }

        [Display(Name = "Chi nhánh")]
        public Branch? branch { get; set; }

        // IBaseAddress
        [Display(Name = "Địa chỉ 1")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Địa chỉ 2")]
        [StringLength(50)]
        public string street2 { get; set; }

        [Display(Name = "Thành phố")]
        [StringLength(30)]
        public string city { get; set; }

        [Display(Name = "Tỉnh/Thành")]
        [StringLength(30)]
        public string province { get; set; }

        [Display(Name = "Quốc gia")]
        [StringLength(30)]

        public string country { get; set; }
        //IBaseAddress
    }
}
