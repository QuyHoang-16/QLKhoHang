using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class TransferOut : INetcoreMasterChild
    {
        public TransferOut()
        {
            this.createdAt = DateTime.UtcNow;
            this.transferOutId = Guid.NewGuid().ToString();
            this.transferOutNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#OUT";
            this.transferOutDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Mã phiếu xuất điều chuyển")]
        public string transferOutId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Mã phiếu điều chuyển")]
        public string transferOrderId { get; set; }

        [Display(Name = "Phiếu điều chuyển")]
        public TransferOrder? transferOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Số phiếu xuất điều chuyển")]
        public string transferOutNumber { get; set; }

        [Required]
        [Display(Name = "Ngày xuất điều chuyển")]
        public DateTime transferOutDate { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Mô tả")]
        public string description { get; set; }

        // FROM (Kho gửi)
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

        [Display(Name = "Danh sách chi tiết xuất điều chuyển")]
        public List<TransferOutLine> transferOutLine { get; set; } = new List<TransferOutLine>();

    }
}
