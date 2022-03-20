using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
                if(ProductSr!=null)
                {
                    products = JsonConvert.DeserializeObject<List<BasketViewModel>>(ProductSr);
                }
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
            List<CheckOutViewModel> products = _getBasketItem();
          
            return View(products);
        }
        [HttpPost]
        public IActionResult Checkout(CheckOutViewModel checkOutVM)
        {
            if (!ModelState.IsValid) return View(checkOutVM);

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            if (checkOutVM.Name == null || checkOutVM.Phone == null || checkOutVM.Email == null || checkOutVM.Adress == null || checkOutVM.Surname == null)
            {
                ModelState.AddModelError("", "Bütün Məlumatlar daxil edilməlidir!");
                TempData["Sifarisnull"] = "Bütün Məlumatlar tam doldurulmalıdır";
                return RedirectToAction("Checkout");
            }
            else
            {
                TempData["Sifaris"] = "Sifarişiniz göndərildi";

            }
            Order order = new Order()
            {
                Address = checkOutVM.Adress,
                Name = checkOutVM.Name,
                Surname = checkOutVM.Surname,
                
                Email = checkOutVM.Email,
                Phone = checkOutVM.Phone,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                DeliveryStatus=Enums.OrderDeliveryStatus.Anbarda,
                OrderItems = new List<OrderItem>(),
                Status = Enums.OrderStatus.Gözləmədə
                
            };
            List<BasketViewModel> basketItemsVM = new List<BasketViewModel>();

            if (member != null)
            {
                order.AppUserId = member.Id;
                basketItemsVM = _context.BasketItems.Where(x => x.AppUserId == member.Id).Select(x => new BasketViewModel
                {
                    ProductId = x.Product.Id,
                    Count = x.Count,
                    
                    
                }).ToList();
            }
            else
            {
                string basketItemsStr = HttpContext.Request.Cookies["Basket"];

                if (basketItemsStr != null)
                {
                    basketItemsVM = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketItemsStr);
                }
               

            }

            foreach (var item in basketItemsVM)
            {

                Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);

                if (product == null)
                {
                    ModelState.AddModelError("", "Not found");
                    return View(checkOutVM);
                }

                _addOrderItem(ref order, product, item.Count);
            }

            if (order.OrderItems.Count == 0)
            {
                ModelState.AddModelError("", "NotFound");
                return View(checkOutVM);
            }
            
            _context.Add(order);
            _context.SaveChanges();

            if (member != null)
            {
                _context.BasketItems.RemoveRange(_context.BasketItems.Where(x => x.AppUserId == member.Id));
                _context.SaveChanges();
            }
            else
            {
                Response.Cookies.Delete("BasketItem");
            }

            return RedirectToAction("index", "home");

        }
        private List<CheckOutViewModel> _getBasketItem()
        {
            List<CheckOutViewModel> products = new List<CheckOutViewModel>();
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            if (member == null)
            {
                var ProductSr = HttpContext.Request.Cookies["Basket"];
                if(ProductSr!=null)
                {
                    products = JsonConvert.DeserializeObject<List<CheckOutViewModel>>(ProductSr);
                }
            }
            else
            {
                products = _context.BasketItems.Include(x => x.Product.SubCategory).Include(x => x.Product).ThenInclude(x => x.ProductImages).Select(x =>
                      new CheckOutViewModel
                      {
                          ProductId = x.ProductId,
                          Count = x.Count,
                          Category = x.Product.SubCategory.Name,
                          Adress = member.Adress,
                          Phone = member.PhoneNumber,
                          Name = member.Name,
                          Surname=member.Surname,
                          Email=member.Email,
                          ProductName = x.Product.Name,
                          Price = x.Product.SalePrice,
                          Discount = x.Product.DiscountPrice,
                          Image = x.Product.ProductImages.FirstOrDefault(x => x.IsPoster == true).Image
                      }).ToList();
            }
            return products;

        }
        private void _addOrderItem(ref Order order, Product product, int count)
        {
            OrderItem orderItem = new OrderItem()
            {
                ProductId = product.Id,

                ProdName = product.Name,
                SalePrice = product.SalePrice - (product.SalePrice / 100) * product.DiscountPrice,
                CostPrice=product.CostPrice,
                DiscountPercent=product.DiscountPrice,
                Count = count
            };

            order.OrderItems.Add(orderItem);
            order.TotalAmount += (orderItem.SalePrice - orderItem.DiscountPercent) * orderItem.Count;
        }
        public IActionResult Comment()
        {
            return PartialView("_CommentsPartial", _context.ProductComments.ToList());
        }
    }
}
