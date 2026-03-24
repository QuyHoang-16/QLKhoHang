using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class ShipmentLine : INetcoreBasic
    {
        public ShipmentLine()
        {
            this.shipmentLineId = Guid.NewGuid().ToString();
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã dòng giao hàng")]
        public string shipmentLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã phiếu giao hàng")]
        public string shipmentId { get; set; }

        [Display(Name = "Phiếu giao hàng")]
        public Shipment? shipment { get; set; }

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

        [Display(Name = "Số lượng")]
        public float qty { get; set; }

        [Display(Name = "Số lượng giao")]
        public float qtyShipment { get; set; }

        [Display(Name = "Số lượng tồn kho sau giao")]
        public float qtyInventory { get; set; }

    }
}
