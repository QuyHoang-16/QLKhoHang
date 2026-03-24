using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class Product: INetcoreBasic
    {
        public Product()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã sản phẩm")]
        public string productId { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã hàng hóa")]
        [Required]
        public string productCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string productName { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string description { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã vạch (Barcode)")]
        public string barcode { get; set; }

        [StringLength(50)]
        [Display(Name = "Số serial (Serial Number)")]
        public string serialNumber { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public ProductType productType { get; set; }

        [Display(Name = "Đơn vị tính (UOM)")]
        public UOM uom { get; set; }

    }
}
