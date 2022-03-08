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
    public class ProductController : Controller
    {
        UserManager<AppUser> _userManager;
        MContext _context;
        public ProductController(MContext context,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index(int id)
        {
           
            return View(_context.Products
                .Include(x=>x.ProductImages)
                .Include(x=>x.Design)
                .Include(x=>x.ProductMatreals).ThenInclude(x=>x.Matreal)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x=>x.SubCategory).ThenInclude(x=>x.Category)
                .Where(x=>x.SubCategoryId==id).ToList());
        }
        public IActionResult Detail(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductColors).ThenInclude(x=>x.Color)
                .Include(x=>x.ProductImages)
                .Include(x=>x.Design)
                .Include(x => x.ProductMatreals).ThenInclude(x=>x.Matreal)
                .FirstOrDefault(x => x.Id == id);
/*            List<Product> NewProducts=_context.Products.ToList().FirstOrDefault(x=>x.IsNew)
*/            if(product==null)
            {
                return RedirectToAction("index", "error");
            }
            ProductDetailViewModel productDetail = new ProductDetailViewModel
            {
                Product = product,
                NewProducts = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.Design)
                .Include(x => x.ProductMatreals).ThenInclude(x => x.Matreal)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .ToList()

            };
            return View(productDetail);
        }
        public IActionResult AddtoWish(int id)
        {
            WishListViewModel wishList = null;
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            List<WishListViewModel> wishes = new List<WishListViewModel>();

                string ProductStr;
                if(HttpContext.Request.Cookies["Product"]!=null)
                {
                    ProductStr = HttpContext.Request.Cookies["Product"];
                    wishes = JsonConvert.DeserializeObject<List<WishListViewModel>>(ProductStr);
                    wishList = wishes.FirstOrDefault(x => x.ProductId == id);
                }
                if (wishList == null)
                {
                    wishList = new WishListViewModel
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Image = product.ProductImages.FirstOrDefault(x => x.IsPoster == true).Image,
                        Price = product.SalePrice
                    };
                    wishes.Add(wishList);
                }
                ProductStr = JsonConvert.SerializeObject(wishes);
                HttpContext.Response.Cookies.Append("Product", ProductStr);

            return NoContent();
        }
        public IActionResult WishList()
        {
            var ProductSr = HttpContext.Request.Cookies["Product"];
            List<WishListViewModel> wishList = new List<WishListViewModel>();
            wishList = JsonConvert.DeserializeObject<List<WishListViewModel>>(ProductSr);
            return View(wishList);
        }

    }
}
