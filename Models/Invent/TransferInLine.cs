using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class TransferInLine : INetcoreBasic
    {
        public TransferInLine()
        {

            this.transferInLineId = Guid.NewGuid().ToString();
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã dòng nhập điều chuyển")]
        public string transferInLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã phiếu nhập điều chuyển")]
        public string transferInId { get; set; }

        [Display(Name = "Phiếu nhập điều chuyển")]
        public TransferIn? transferIn { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã sản phẩm")]
        public string productId { get; set; }

        [Display(Name = "Sản phẩm")]
        public Product? product { get; set; }

        [Display(Name = "Số lượng nhập")]
        public float qty { get; set; }

        [Display(Name = "Số lượng tồn kho sau nhập")]
        public float qtyInventory { get; set; }

    }
}
