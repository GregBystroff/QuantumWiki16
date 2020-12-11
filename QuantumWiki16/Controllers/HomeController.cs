using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuantumWiki16.Models;
using System.Diagnostics;

namespace QuantumWiki16.Controllers
{
    public class HomeController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        private readonly ILogger<HomeController> _logger;

        //   C o n s t r u c t o r s

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //   M e t h o d s


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
