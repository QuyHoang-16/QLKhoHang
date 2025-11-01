using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models
{
    public class INetcoreBasic
    {
        [Display(Name = "Created At")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
