using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class PurchaseOrder : INetcoreMasterChild
    {
        public PurchaseOrder()
        {
            this.createdAt = DateTime.UtcNow;
            this.purchaseOrderId = Guid.NewGuid().ToString();
            this.purchaseOrderNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#PO";
            this.poDate = DateTime.UtcNow.Date;
            this.deliveryDate = this.poDate.AddDays(5);
            this.purchaseOrderStatus = PurchaseOrderStatus.Draft;
            this.totalDiscountAmount = 0m;
            this.totalOrderAmount = 0m;
            this.purchaseReceiveNumber = string.Empty;
        }

        [StringLength(38)]
        [Display(Name = "Mã đơn mua hàng")]
        public string purchaseOrderId { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Số PO")]
        public string purchaseOrderNumber { get; set; }

        [Display(Name = "Điều khoản thanh toán (TOP)")]
        public TOP top { get; set; }

        [Display(Name = "Ngày tạo PO")]
        public DateTime poDate { get; set; }

        [Display(Name = "Ngày giao hàng")]
        public DateTime deliveryDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ giao hàng")]
        public string deliveryAddress { get; set; }

        [StringLength(30)]
        [Display(Name = "Số tham chiếu nội bộ")]
        public string referenceNumberInternal { get; set; }

        [StringLength(30)]
        [Display(Name = "Số tham chiếu từ nhà cung cấp")]
        public string referenceNumberExternal { get; set; }

        [StringLength(100)]
        [Display(Name = "Mô tả")]
        public string description { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Mã chi nhánh")]
        public string branchId { get; set; }

        [Display(Name = "Chi nhánh")]
        public Branch? branch { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Mã nhà cung cấp")]
        public string vendorId { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public Vendor? vendor { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Người phụ trách nội bộ (PIC nội bộ)")]
        public string picInternal { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Người phụ trách nhà cung cấp (PIC NCC)")]
        public string picVendor { get; set; }

        [Display(Name = "Trạng thái đơn mua hàng")]
        public PurchaseOrderStatus purchaseOrderStatus { get; set; }

        [Display(Name = "Tổng giảm giá")]
        public decimal totalDiscountAmount { get; set; }

        [Display(Name = "Tổng giá trị đơn hàng")]
        public decimal totalOrderAmount { get; set; }

        [Display(Name = "Số phiếu nhận hàng (PR)")]
        public string? purchaseReceiveNumber { get; set; }

        [Display(Name = "Danh sách chi tiết đơn hàng")]
        public List<PurchaseOrderLine> purchaseOrderLine { get; set; } = new List<PurchaseOrderLine>();

    }
}
