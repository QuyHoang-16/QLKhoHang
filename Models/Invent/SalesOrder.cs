using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class SalesOrder : INetcoreMasterChild
    {
        public SalesOrder()
        {
            this.createdAt = DateTime.UtcNow;
            this.salesOrderId = Guid.NewGuid().ToString();
            this.salesOrderNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#SO";
            this.soDate = DateTime.UtcNow.Date;
            this.deliveryDate = this.soDate.AddDays(5);
            this.salesOrderStatus = SalesOrderStatus.Draft;
            this.totalDiscountAmount = 0m;
            this.totalOrderAmount = 0m;
            this.salesShipmentNumber = string.Empty;
        }

        [StringLength(38)]
        [Display(Name = "Mã đơn bán hàng")]
        public string salesOrderId { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Số SO")]
        public string salesOrderNumber { get; set; }

        [Display(Name = "Điều khoản thanh toán (TOP)")]
        public TOP top { get; set; }

        [Display(Name = "Ngày tạo SO")]
        public DateTime soDate { get; set; }

        [Display(Name = "Ngày giao hàng dự kiến")]
        public DateTime deliveryDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ giao hàng")]
        public string deliveryAddress { get; set; }

        [StringLength(30)]
        [Display(Name = "Số tham chiếu nội bộ")]
        public string referenceNumberInternal { get; set; }

        [StringLength(30)]
        [Display(Name = "Số tham chiếu từ khách hàng")]
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
        [Display(Name = "Mã khách hàng")]
        public string customerId { get; set; }

        [Display(Name = "Khách hàng")]
        public Customer? customer { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Người phụ trách nội bộ (PIC nội bộ)")]
        public string picInternal { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Người phụ trách khách hàng (PIC KH)")]
        public string picCustomer { get; set; }

        [Display(Name = "Trạng thái đơn bán hàng")]
        public SalesOrderStatus salesOrderStatus { get; set; }

        [Display(Name = "Tổng giảm giá")]
        public decimal totalDiscountAmount { get; set; }

        [Display(Name = "Tổng giá trị đơn hàng")]
        public decimal totalOrderAmount { get; set; }

        [Display(Name = "Số phiếu giao hàng")]
        public string? salesShipmentNumber { get; set; }

        [Display(Name = "Danh sách chi tiết đơn hàng")]
        public List<SalesOrderLine> salesOrderLine { get; set; } = new List<SalesOrderLine>();

    }
}
