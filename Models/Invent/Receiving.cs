using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class Receiving : INetcoreMasterChild
    {
        public Receiving()
        {
            this.createdAt = DateTime.UtcNow;
            this.receivingId = Guid.NewGuid().ToString();
            this.receivingNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#GSRN";
            this.receivingDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã phiếu nhận hàng")]
        public string receivingId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Mã đơn mua hàng")]
        public string purchaseOrderId { get; set; }

        [Display(Name = "Đơn mua hàng")]
        public PurchaseOrder? purchaseOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Số phiếu GSRN")]
        public string receivingNumber { get; set; }

        [Required]
        [Display(Name = "Ngày nhận hàng")]
        public DateTime receivingDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã nhà cung cấp")]
        public string vendorId { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public Vendor? vendor { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Số DO của nhà cung cấp")]
        public string vendorDO { get; set; }

        [StringLength(50)]
        [Display(Name = "Số hóa đơn của nhà cung cấp")]
        public string vendorInvoice { get; set; }

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

        [Display(Name = "Danh sách chi tiết nhận hàng")]
        public List<ReceivingLine> receivingLine { get; set; } = new List<ReceivingLine>();

    }
}
