using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuantumWiki16.Controllers
{
    public class TutorialController : Controller
    {
        public IActionResult Index()  // list of tutorials
        {
            return View();
        }
    }
}
