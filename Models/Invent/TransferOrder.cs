using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class TransferOrder : INetcoreMasterChild
    {
        public TransferOrder()
        {
            
            this.createdAt = DateTime.UtcNow;
            this.transferOrderId = Guid.NewGuid().ToString();
            this.transferOrderNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#TO";
            this.transferOrderDate = DateTime.UtcNow;
            this.transferOrderStatus = TransferOrderStatus.Draft;
            this.isIssued = false;
            this.isReceived = false;
        }

        [StringLength(38)]
        [Display(Name = "Mã phiếu điều chuyển")]
        public string transferOrderId { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Số phiếu điều chuyển")]
        public string transferOrderNumber { get; set; }

        [Required]
        [Display(Name = "Ngày điều chuyển")]
        public DateTime transferOrderDate { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Mô tả")]
        public string description { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Người phụ trách (PIC)")]
        public string picName { get; set; }

        // FROM (Kho xuất)
        [StringLength(38)]
        [Display(Name = "Mã chi nhánh gửi")]
        public string branchIdFrom { get; set; }

        [Display(Name = "Chi nhánh gửi")]
        public Branch? branchFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã kho gửi")]
        public string warehouseIdFrom { get; set; }

        [Display(Name = "Kho gửi")]
        public Warehouse? warehouseFrom { get; set; }

        // TO (Kho nhận)
        [StringLength(38)]
        [Display(Name = "Mã chi nhánh nhận")]
        public string branchIdTo { get; set; }

        [Display(Name = "Chi nhánh nhận")]
        public Branch? branchTo { get; set; }

        [StringLength(38)]
        [Display(Name = "Mã kho nhận")]
        public string warehouseIdTo { get; set; }

        [Display(Name = "Kho nhận")]
        public Warehouse? warehouseTo { get; set; }

        [Display(Name = "Trạng thái phiếu điều chuyển")]
        public TransferOrderStatus transferOrderStatus { get; set; }

        [Display(Name = "Đã xuất kho?")]
        public bool isIssued { get; set; }

        [Display(Name = "Đã nhập kho?")]
        public bool isReceived { get; set; }

        [Display(Name = "Danh sách chi tiết điều chuyển")]
        public List<TransferOrderLine> transferOrderLine { get; set; } = new List<TransferOrderLine>();

    }
}
