using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class SalesOrderLine : INetcoreBasic
    {
        public SalesOrderLine()
        {
            this.createdAt = DateTime.UtcNow;
            this.discountAmount = 0m;
            this.totalAmount = 0m;
        }

        [StringLength(38)]
        [Display(Name = "Mã dòng đơn bán hàng")]
        public string salesOrderLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã đơn bán hàng")]
        public string salesOrderId { get; set; }

        [Display(Name = "Đơn bán hàng")]
        public SalesOrder? salesOrder { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã sản phẩm")]
        public string productId { get; set; }

        [Display(Name = "Sản phẩm")]
        public Product? product { get; set; }

        [Display(Name = "Số lượng")]
        public float qty { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal price { get; set; }

        [Display(Name = "Số tiền giảm giá")]
        public decimal discountAmount { get; set; }

        [Display(Name = "Thành tiền")]
        public decimal totalAmount { get; set; }

    }
}
