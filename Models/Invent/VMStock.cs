using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKho.Models.Invent
{
    public class VMStock
    {
        [Display(Name = "Sản phẩm")]
        public string Product { get; set; }

        [Display(Name = "Kho hàng")]
        public string Warehouse { get; set; }

        [Display(Name = "SL đặt mua (PO)")]
        public float QtyPO { get; set; }

        [Display(Name = "SL đã nhận (Receiving)")]
        public float QtyReceiving { get; set; }

        [Display(Name = "SL bán (SO)")]
        public float QtySO { get; set; }

        [Display(Name = "SL giao hàng (Shipment)")]
        public float QtyShipment { get; set; }

        [Display(Name = "SL nhập điều chuyển")]
        public float QtyTransferIn { get; set; }

        [Display(Name = "SL xuất điều chuyển")]
        public float QtyTransferOut { get; set; }

        [Display(Name = "SL tồn kho hiện tại")]
        public float QtyOnhand { get; set; }

    }
}
