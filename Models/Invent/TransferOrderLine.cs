using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class TransferOrderLine : INetcoreBasic
    {
        public TransferOrderLine()
        {

            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã dòng điều chuyển")]
        public string transferOrderLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã phiếu điều chuyển")]
        public string transferOrderId { get; set; }

        [Display(Name = "Phiếu điều chuyển")]
        public TransferOrder? transferOrder { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã sản phẩm")]
        public string productId { get; set; }

        [Display(Name = "Sản phẩm")]
        public Product? product { get; set; }

        [Display(Name = "Số lượng điều chuyển")]
        public float qty { get; set; }

    }
}
