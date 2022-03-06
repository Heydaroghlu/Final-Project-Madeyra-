using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
    public class HomeController : Controller
    {
        MContext _context;
        public HomeController(MContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeView = new HomeViewModel
            {
                Settings = _context.Settings.FirstOrDefault(),
                Products = _context.Products.Include(x => x.ProductMatreals).Include(x => x.ProductImages)
                .Include(x => x.ProductColors).ToList()
                
            };
            return View(homeView);
        }
        public IActionResult Basket()
        {
            return View();
        }


    }
}
