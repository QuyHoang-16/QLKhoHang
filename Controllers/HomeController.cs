using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyKho.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace QuanLyKho.Controllers
{
    [Authorize(Roles = "Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing Home Index");

            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        [Authorize]
        public IActionResult About()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing Home About");

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing Home Contact");

            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class Home
        {
            public const string Controller = "Home";
            public const string Action = "Index";
            public const string Role = "Home";
            public const string Url = "/Home/Index";
            public const string Name = "Home";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Home")]
        public bool HomeRole { get; set; } = false;
    }
}
