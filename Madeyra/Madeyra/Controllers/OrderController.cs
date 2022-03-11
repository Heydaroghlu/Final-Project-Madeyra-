using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
    public class OrderController : Controller
    {
        private readonly MContext _context;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(MContext context,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Basket()
        {
            List<BasketViewModel> products = new List<BasketViewModel>();
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            if(member==null)
            {
                var ProductSr = HttpContext.Request.Cookies["Basket"];
                products = JsonConvert.DeserializeObject<List<BasketViewModel>>(ProductSr);
                return View(products);
            }
            else
            {
                products = _context.BasketItems.Include(x=>x.Product).ThenInclude(x=>x.ProductImages).Select(x =>
                new BasketViewModel
                {
                    ProductId = x.ProductId,
                    Count = x.Count,
                    ProductName = x.Product.Name,
                    Price = x.Product.SalePrice,
                    Discount=x.Product.DiscountPrice,
                    Image = x.Product.ProductImages.FirstOrDefault(x=>x.IsPoster==true).Image
                }).ToList();
            }
            return View(products);

        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
