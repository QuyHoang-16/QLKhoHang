using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class TransferOutLine : INetcoreBasic
    {
        public TransferOutLine()
        {
            this.transferOutLineId = Guid.NewGuid().ToString();

            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã dòng xuất điều chuyển")]
        public string transferOutLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã phiếu xuất điều chuyển")]
        public string transferOutId { get; set; }

        [Display(Name = "Phiếu xuất điều chuyển")]
        public TransferOut? transferOut { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã sản phẩm")]
        public string productId { get; set; }

        [Display(Name = "Sản phẩm")]
        public Product? product { get; set; }

        [Display(Name = "Số lượng xuất")]
        public float qty { get; set; }

        [Display(Name = "Số lượng tồn kho sau xuất")]
        public float qtyInventory { get; set; }

    }
}
