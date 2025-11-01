using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize(Roles = "Stock")]
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class Stock
        {
            public const string Controller = "Stock";
            public const string Action = "Index";
            public const string Role = "Stock";
            public const string Url = "/Stock/Index";
            public const string Name = "Stock";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Stock")]
        public bool StockRole { get; set; } = false;
    }
}
