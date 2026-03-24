using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class Customer : INetcoreMasterChild, IBaseAddress
    {
        public Customer()
        {
            this.createdAt = DateTime.UtcNow;
            this.customerId = Guid.NewGuid().ToString();
        }

        [StringLength(38)]
        [Display(Name = "Mã khách hàng")]
        public string customerId { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên khách hàng")]
        [Required]
        public string customerName { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string description { get; set; }

        [Display(Name = "Quy mô doanh nghiệp")]
        public BusinessSize size { get; set; }

        //IBaseAddress
        [Display(Name = "Địa chỉ (Số nhà / Đường 1)")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Địa chỉ bổ sung (Đường 2)")]
        [StringLength(50)]
        public string street2 { get; set; }

        [Display(Name = "Thành phố")]
        [StringLength(30)]
        public string city { get; set; }

        [Display(Name = "Tỉnh / Thành")]
        [StringLength(30)]
        public string province { get; set; }

        [Display(Name = "Quốc gia")]
        [StringLength(30)]
        public string country { get; set; }
        //IBaseAddress

        [Display(Name = "Danh sách liên hệ khách hàng")]
        public List<CustomerLine> customerLine { get; set; } = new List<CustomerLine>();
    }
}
