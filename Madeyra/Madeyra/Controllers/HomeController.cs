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
                Cards = _context.Cards.ToList(),
                Products = _context.Products.Include(x => x.ProductMatreals).Include(x => x.ProductImages)
                .Include(x => x.ProductColors).ToList(),
                Sliders = _context.Sliders.ToList(),
                EndOrders=_context.Orders.Include(x=>x.OrderItems).ThenInclude(x=>x.Product).OrderByDescending(x=>x.Id).Take(5).Where(x=>x.Status==Enums.OrderStatus.Qəbul).ToList()
            };
            return View(homeView);
        }
        public IActionResult Basket()
        {
            return View();
        }
         public IActionResult Search(string search1)
        {
            List<Product> Searchs = _context.Products.Include(x => x.ProductMatreals).Include(x => x.ProductImages)
                .Include(x => x.ProductColors).Where(x => x.Name.Contains(search1)).ToList();
            return PartialView("_SearchPartial", Searchs);
        }

    }
}
