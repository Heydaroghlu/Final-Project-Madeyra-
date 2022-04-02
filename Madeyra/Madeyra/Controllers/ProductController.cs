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
    public class ProductController : Controller
    {
        UserManager<AppUser> _userManager;
        MContext _context;
        public ProductController(MContext context,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index(int id,decimal? min=null,decimal? max=null)
        {
            var products = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.Design)
                .Include(x => x.ProductMatreals).ThenInclude(x => x.Matreal)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .Where(x => x.SubCategoryId == id).AsQueryable();
            if(products!=null && products.Count()>0)
            {
                ViewBag.Min = (int)products.Min(x => x.SalePrice);
                ViewBag.Max = (int)products.Max(x => x.SalePrice);
            }
            if(min!=null && max!=null)
            {
                products = products.Where(x => x.SalePrice >= min && x.SalePrice <= max);
            }
            ViewBag.SelectMin = min ?? ViewBag.Min;
            ViewBag.SelectMax = max ?? ViewBag.Max;
            return View(products.ToList());
        }
        public IActionResult Detail(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductColors).ThenInclude(x=>x.Color)
                .Include(x=>x.ProductImages)
                .Include(x=>x.Design)
                .Include(x => x.ProductMatreals).ThenInclude(x=>x.Matreal)
                .Include(x=>x.ProductComments)
                .FirstOrDefault(x => x.Id == id);
/*            List<Product> NewProducts=_context.Products.ToList().FirstOrDefault(x=>x.IsNew)
*/            if(product==null)
            {
                return RedirectToAction("index", "error");
            }
            AppUser user = null;
            if(User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin==false);
            }
             
            ProductDetailViewModel productDetail = new ProductDetailViewModel
            {
                Product = product,
                News = _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.Design)
                .Include(x => x.ProductMatreals).ThenInclude(x => x.Matreal)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .ToList(),
                ProductComment=new ProductComment
                {AppUser=user },
                EndOrders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).OrderByDescending(x => x.Id).Take(5).Where(x => x.Status == Enums.OrderStatus.Qəbul).ToList()

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
                        Price = product.SalePrice,
                        Count=product.Count
                    };
                    wishes.Add(wishList);
                }
                ProductStr = JsonConvert.SerializeObject(wishes);
                HttpContext.Response.Cookies.Append("Product", ProductStr);
            return NoContent();
        }
       
        public IActionResult WishList()
        {
            AppUser member = null;
            if(User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.NormalizedUserName == User.Identity.Name.ToUpper());

            }
            if (member==null || member.IsAdmin==true)
            {
                return RedirectToAction("login", "account");
            }
            var ProductSr = HttpContext.Request.Cookies["Product"];
            List<WishListViewModel> wishList = new List<WishListViewModel>();
            if(ProductSr!=null)
            {
                wishList = JsonConvert.DeserializeObject<List<WishListViewModel>>(ProductSr);
            }
            return View(wishList);
        }   
        public  IActionResult DeleteWish(int id)
        {
            WishListViewModel wishList = null;
            List<WishListViewModel> wishListViews = new List<WishListViewModel>();
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            string ProductStr = HttpContext.Request.Cookies["Product"];
            wishListViews = JsonConvert.DeserializeObject<List<WishListViewModel>>(ProductStr);

            wishList = wishListViews.FirstOrDefault(x => x.ProductId == id);
            wishListViews.Remove(wishList);
            ProductStr = JsonConvert.SerializeObject(wishListViews);
            HttpContext.Response.Cookies.Append("Product", ProductStr);
            TempData["Error"] = "Məhsul Arzu siyahınızdan silindi";

            return RedirectToAction("wishlist");
        }

        public IActionResult AddtoBasket(int id)
        {
           

            Product product = _context.Products.Include(x=>x.SubCategory).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            BasketViewModel basketItem = null;
            if(product==null)
            {
                return NotFound();
            }
            AppUser member = null;
            if(User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            List<BasketViewModel> products = new List<BasketViewModel>();
            if(member==null)
            {
                string productStr;
                if (HttpContext.Request.Cookies["Basket"] != null)
                {
                    productStr = HttpContext.Request.Cookies["Basket"];
                    products = JsonConvert.DeserializeObject<List<BasketViewModel>>(productStr);
                    basketItem = products.FirstOrDefault(x => x.ProductId == id);
                }

                if (basketItem == null)
                {
                    basketItem = new BasketViewModel

                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Image = product.ProductImages.FirstOrDefault(x => x.IsPoster == true).Image,
                        Price = product.SalePrice,
                        Category=product.SubCategory.Name,
                        Discount=product.DiscountPrice,
                        Count = 1,
                    };
                    products.Add(basketItem);

                }
                else
                {
                    basketItem.Count++;
                }
                productStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("Basket", productStr);


            }
            else
            {
                BasketItem memberBasketItem = _context.BasketItems.FirstOrDefault
                    (x => x.AppUserId == member.Id && x.ProductId == id);
                if(memberBasketItem==null)
                {
                    memberBasketItem = new BasketItem
                    {
                        AppUserId = member.Id,
                        ProductId = id,
                        Count = 1,
                        Discount=product.DiscountPrice,
                        Price=product.SalePrice
                    };
                    _context.BasketItems.Add(memberBasketItem);

                }
                else
                {
                    memberBasketItem.Count++;
                }
                _context.SaveChanges();
                products = _context.BasketItems.Select(x =>
                  new BasketViewModel
                  { 
                  ProductId=x.ProductId,
                  Count=x.Count,
                  Discount=x.Discount,
                  ProductName=x.Product.Name,
                  Price=x.Product.SalePrice,
                      Image = x.Product.ProductImages
                                .FirstOrDefault(x => x.IsPoster == true).Image
                  }).ToList();
                       
            }

            return PartialView("_BasketPartial",products);

        }
        public IActionResult RemoveBasket(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            BasketViewModel basketItem = null;
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            }
            List<BasketViewModel> products = new List<BasketViewModel>();
            if (member == null)
            {
                string ProductStr = HttpContext.Request.Cookies["Basket"];
                products = JsonConvert.DeserializeObject<List<BasketViewModel>>(ProductStr);

                basketItem = products.FirstOrDefault(x => x.ProductId == id);

                if (basketItem.Count == 1)
                { products.Remove(basketItem); }
                else { basketItem.Count--; }



                ProductStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("Basket", ProductStr);
            }
            else
            {
                BasketItem memberBasketItem = _context.BasketItems.Include(x => x.Product).FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);


                if (memberBasketItem.Count == 1)
                { _context.BasketItems.Remove(memberBasketItem); }
                else { memberBasketItem.Count--; }
                _context.SaveChanges();

                products = _context.BasketItems.Include(x => x.Product).ThenInclude(bi => bi.ProductImages).Where(x => x.AppUserId == member.Id)
                    .Select(x => new BasketViewModel { ProductId = x.ProductId, Count = x.Count, ProductName = x.Product.Name, Price = x.Product.SalePrice, Image = x.Product.ProductImages.FirstOrDefault(b => b.IsPoster == true).Image }).ToList();

            }
            return PartialView("_BasketPartial", products);

        }
        public IActionResult ClearBasket()
        {
            BasketViewModel basketItem = null;
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            }
            if (member != null)
            {
                BasketItem basket = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id);
                if (basket != null)
                {
                    _context.BasketItems.Remove(basket);
                    _context.SaveChanges();
                }

            }
            else
            {
                List<BasketViewModel> products = new List<BasketViewModel>();
                if(HttpContext.Request.Cookies["Basket"]!=null)
                {
                    string ProductStr = HttpContext.Request.Cookies["Basket"];
                    products = JsonConvert.DeserializeObject<List<BasketViewModel>>(ProductStr);

                    products.RemoveAll(x => x.Price > 0);
                    ProductStr = JsonConvert.SerializeObject(products);
                    HttpContext.Response.Cookies.Append("Basket", ProductStr);
                }
        
            }

            return RedirectToAction("basket","order");
        }
        [HttpPost]
        public async Task<IActionResult> Comment(ProductComment comment)
        {
           /* if(comment.Name==null || comment.SurName==null || comment.Text==null)
            {
                TempData["Sifarisnull"] = "Şərh məlumatları düzgün deyil";
                return RedirectToAction("Detail",new { id=comment.ProductId});
            }*/
            Product product = _context.Products.Include(x => x.ProductImages)
                .Include(x => x.ProductColors)
                .Include(x => x.ProductMatreals)
                .FirstOrDefault(x => x.Id == comment.ProductId && !x.IsDeleted);
            if (product == null)
            {
                return View("index", "error");
            }

            ProductDetailViewModel detailView = new ProductDetailViewModel
            {
                Product = product,
                ProductComment = comment,
                News = _context.Products.Include(x => x.ProductImages)
                .Include(x => x.ProductColors)
                .Include(x => x.ProductMatreals).Where(x => x.SubCategoryId == product.SubCategoryId).ToList()
/*                EndOrderd = _context.Products.Where(x => x.Id == _context.Orders.Last().OrderItems.First().ProductId).ToList()
*/            };
            if(!ModelState.IsValid)
            {
                TempData["CommentError"] = "Comment gonderilmedi";
                return View("detail", detailView);
            }
            if (!_context.Products.Any(x => x.Id == comment.ProductId))
            {
                TempData["CommentError"] = "Bele bir Mehsul yoxdur";
                return View("Detail", detailView);
            }
            if (!User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrWhiteSpace(comment.Name))
                {
                    TempData["error"] = "Email is required";
                    return View("Detail", detailView);
                }

                if (string.IsNullOrWhiteSpace(comment.SurName))
                {
                    TempData["error"] = "FullName is required";
                    return View("Detail", detailView);
                }
            }
            else
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                comment.AppUserId = user.Id;
                comment.AppUser = user;
                comment.Name = user.Name;
                comment.SurName = user.Surname;
            }
            comment.Status = false;
            comment.SendTime = DateTime.UtcNow.AddHours(4);
            _context.ProductComments.Add(comment);
            _context.SaveChanges();

            TempData["Success"] = "Şərhiniz göndərilid";

            return RedirectToAction("detail",new {Id=product.Id });
        }


    }
}
