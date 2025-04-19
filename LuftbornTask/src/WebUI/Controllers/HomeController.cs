using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LuftbornTask.src.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int statusCode)
        {
            ViewData["StatusCode"] = statusCode;
            return View();
        }
    }
}
