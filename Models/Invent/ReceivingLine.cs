using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class ReceivingLine : INetcoreBasic
    {
        public ReceivingLine()
        {
            this.receivingLineId = Guid.NewGuid().ToString();
            this.createdAt = DateTime.UtcNow;
        }


        [StringLength(38)]
        [Display(Name = "Mã dòng nhận hàng")]
        public string receivingLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã phiếu nhận hàng")]
        public string receivingId { get; set; }

        [Display(Name = "Phiếu nhận hàng")]
        public Receiving? receiving { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã chi nhánh")]
        public string branchId { get; set; }

        [Display(Name = "Chi nhánh")]
        public Branch? branch { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã kho")]
        public string warehouseId { get; set; }

        [Display(Name = "Kho hàng")]
        public Warehouse? warehouse { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã sản phẩm")]
        public string productId { get; set; }

        [Display(Name = "Sản phẩm")]
        public Product? product { get; set; }

        [Display(Name = "Số lượng đặt (PO)")]
        public float qty { get; set; }

        [Display(Name = "Số lượng nhận")]
        public float qtyReceive { get; set; }

        [Display(Name = "Số lượng tồn kho sau nhận")]
        public float qtyInventory { get; set; }

    }
}
