using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Termin()
        {
            return View();
        }
        public IActionResult Quaranty()
        {
            return View();
        }
        public IActionResult Mission()
        {
            return View();
        }
    }
}
