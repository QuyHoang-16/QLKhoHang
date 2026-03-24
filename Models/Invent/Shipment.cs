using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class Shipment : INetcoreMasterChild
    {
        public Shipment()
        {
            this.createdAt = DateTime.UtcNow;
            this.shipmentId = Guid.NewGuid().ToString();
            this.shipmentNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#DO";
            this.shipmentDate = DateTime.UtcNow;
            this.expeditionType = ExpeditionType.Internal;
            this.expeditionMode = ExpeditionMode.Land;
        }

        [StringLength(38)]
        [Display(Name = "Mã phiếu giao hàng")]
        public string shipmentId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Mã đơn bán hàng")]
        public string salesOrderId { get; set; }

        [Display(Name = "Đơn bán hàng")]
        public SalesOrder? salesOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Số DO (Delivery Order)")]
        public string shipmentNumber { get; set; }

        [Required]
        [Display(Name = "Ngày giao hàng")]
        public DateTime shipmentDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã khách hàng")]
        public string customerId { get; set; }

        [Display(Name = "Khách hàng")]
        public Customer? customer { get; set; }

        [StringLength(50)]
        [Display(Name = "Số PO của khách hàng")]
        public string customerPO { get; set; }

        [StringLength(50)]
        [Display(Name = "Số hóa đơn (Invoice)")]
        public string invoice { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã chi nhánh")]
        public string branchId { get; set; }

        [Display(Name = "Chi nhánh")]
        public Branch? branch { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Mã kho")]
        public string warehouseId { get; set; }

        [Display(Name = "Kho hàng")]
        public Warehouse? warehouse { get; set; }

        [Display(Name = "Loại hình vận chuyển")]
        public ExpeditionType expeditionType { get; set; }

        [Display(Name = "Phương thức vận chuyển")]
        public ExpeditionMode expeditionMode { get; set; }

        [Display(Name = "Danh sách chi tiết giao hàng")]
        public List<ShipmentLine> shipmentLine { get; set; } = new List<ShipmentLine>();

    }
}
