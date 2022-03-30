using Madeyra.Helpers;
using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class ProductController : Controller
    {
        MContext _context;
        IWebHostEnvironment _env;
        public ProductController(MContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? deleted = null, string? search=null)
        {
            var products = _context.Products
                .Include(x => x.SubCategory)
                .Include(x => x.ProductMatreals).
                Include(x=>x.ProductColors).
                Include(x => x.ProductImages).AsQueryable();
                 if (deleted == false)
            {
                products = products.Where(x => x.IsNew == true);
            }
            if (deleted == true)
            {
                products = products.Where(x => x.IsDeleted == true);
            }
            if(search!=null)
            {
                products = products.Where(x => x.Name.Contains(search));
            }
            
            ViewBag.IsDeleted = deleted;
            ViewBag.Search = search;
            return View(products.ToPagedList(page, 5));
        }
        public IActionResult Create()
        {
            ViewBag.SubCategory = _context.SubCategories.ToList().Where(x => x.IsDeleted == false); ;
            ViewBag.ColorIds = _context.Colors.ToList().Where(x => x.IsDeleted == false); ;
            ViewBag.DesignId = _context.Designs.ToList().Where(x=>x.IsDeleted==false);
            ViewBag.MatrealId = _context.Matreals.ToList().Where(x => x.IsDeleted == false);
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.SubCategory = _context.SubCategories.Where(x => x.IsDeleted == false).ToList();
            ViewBag.ColorIds = _context.Colors.Where(x => x.IsDeleted == false).ToList();
            ViewBag.DesignId = _context.Designs.Where(x => x.IsDeleted == false).ToList();
            ViewBag.MatrealId = _context.Matreals.Where(x => x.IsDeleted == false).ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(product.ColorIds !=null)
            {
                product.ProductColors = new List<ProductColor>();
                foreach (var colorId in product.ColorIds)
                {
                    Color color = _context.Colors.FirstOrDefault(x => x.Id == colorId);
                    if(color==null)
                    {
                        ModelState.AddModelError("ColorIds", "Databaza da belə bir rəng tapılmadı");
                        return View();
                    }
                    ProductColor productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product
                    };
                    product.ProductColors.Add(productColor);
                }
            }
            if (product.MatrealIds != null)
            {
                product.ProductMatreals = new List<ProductMatreal>();
                foreach (var matrealId in product.MatrealIds)
                {
                    Matreal matreal = _context.Matreals.FirstOrDefault(x => x.Id == matrealId);
                    if (matreal == null)
                    {
                        ModelState.AddModelError("MatrealIds", "Databaza da belə bir matreal tapılmadı");
                        return View();
                    }
                    ProductMatreal productMatreal = new ProductMatreal
                    {
                       MatrealId = matrealId ,
                        Product = product
                    };
                    product.ProductMatreals.Add(productMatreal);
                }
            }
            product.ProductImages = new List<ProductImage>();
            if(product.PosterImage==null)
            {
                ModelState.AddModelError("PosterImage", "Poster şəkli boş ola bilməz!");
                return View();
            }
            else
            {
                if (product.PosterImage.ContentType !="image/jpeg" && product.PosterImage.ContentType!="image/png")
                {
                    ModelState.AddModelError("PosterImage", "Şəklin formatı düzgün deyil!");
                    return View();
                }
                if(product.PosterImage.Length>2097152)
                {
                    ModelState.AddModelError("PosterImage", "Şəklin ölçüsü 2mb dan artıq ola bilməz!");
                    return View();
                }
                string filename = FileManager.Save(_env.WebRootPath, "uploads/product", product.PosterImage);
                ProductImage productImage = new ProductImage
                {
                    Image = filename,
                    IsPoster = true
                };
                product.ProductImages.Add(productImage);
            }
            if(product.ImageFiles!=null)
            {
                foreach (var image in product.ImageFiles)
                {
                    if(image.ContentType!="image/png" && image.ContentType!="image/jpeg")
                    {
                        continue;
                    }
                    if(image.Length>2097152)
                    {
                        continue;
                    }
                    ProductImage productImage = new ProductImage
                    {
                        IsPoster = false,
                        Image = FileManager.Save(_env.WebRootPath, "uploads/product", image)
                    };
                    if(product.ProductImages==null)
                    {
                        product.ProductImages = new List<ProductImage>();
                    }
                    product.ProductImages.Add(productImage);

                }
            }
            if(product.SalePrice<0)
            {
                ModelState.AddModelError("Price", "Qiymət mənfi ola bilməz");
                return View();

            }
            if (product.CostPrice < 0)
            {
                ModelState.AddModelError("Price", "Qiymət mənfi ola bilməz");
                return View();

            }
            if (product.DiscountPrice < 0)
            {
                ModelState.AddModelError("Price", "Qiymət mənfi ola bilməz");
                return View();

            }
            if (product.Count < 0)
            {
                ModelState.AddModelError("Price", "Say mənfi ola bilməz");
                return View();

            }
            if (!ModelState.IsValid) return View();
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Update(int id)
        {
            Product product = _context.Products.Include(x => x.ProductMatreals)
                .Include(x => x.ProductColors)
                .Include(x=>x.ProductImages)
                .FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return RedirectToAction("index", "error");
            }
            ViewBag.SubCategory = _context.SubCategories.Where(x => x.IsDeleted == false).ToList();
            ViewBag.ColorIds = _context.Colors.Where(x=>x.IsDeleted==false).ToList();
            ViewBag.DesignId = _context.Designs.Where(x => x.IsDeleted == false).ToList();
            ViewBag.MatrealId = _context.Matreals.Where(x => x.IsDeleted == false).ToList();
            product.MatrealIds = product.ProductMatreals.Select(x => x.MatrealId).ToList();
            product.ColorIds = product.ProductColors.Select(x => x.ColorId).ToList();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
         
            
            Product oldProduct = _context.Products.Include(x => x.SubCategory).Include(x => x.ProductColors)
                 .Include(x => x.ProductImages).Include(x => x.ProductMatreals).FirstOrDefault(x => x.Id == product.Id);
           
            if (oldProduct==null)
            {
                return RedirectToAction("index", "error");
            }
            if (product.PosterImage != null)
            {
                if (product.PosterImage.ContentType != "image/jpeg" && product.PosterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("PosterImage", "Şəklin formatı düzgün deyil!");
                    return View();
                }
                if (product.PosterImage.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImage", "Şəklin ölçüsü 2mb dan artıq ola bilməz!");
                    return View();
                }
                ProductImage posterImage = _context.ProductImages.Where(x => x.ProductId == product.Id).FirstOrDefault(x => x.IsPoster == true);
                string filename = FileManager.Save(_env.WebRootPath, "uploads/product", product.PosterImage);
                if (posterImage == null)
                {
                    posterImage = new ProductImage
                    {
                        IsPoster = true,
                        Image = filename,
                        ProductId = product.Id
                    };
                    _context.ProductImages.Add(posterImage);
                }
                else
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/product", posterImage.Image);
                    posterImage.Image = filename;
                }
            }
            oldProduct.ProductImages.RemoveAll(x => x.IsPoster == false && !product.ProductImageIds.Contains(x.Id));

            if (product.ImageFiles != null)
            {
                foreach (var file in product.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        continue;
                    }

                    if (file.Length > 2097152)
                    {
                        continue;
                    }

                    ProductImage image = new ProductImage
                    {
                        IsPoster = false,
                        Image = FileManager.Save(_env.WebRootPath, "uploads/product", file)
                    };
                    if (oldProduct.ProductImages == null)
                        oldProduct.ProductImages = new List<ProductImage>();
                    oldProduct.ProductImages.Add(image);
                }
            }
            oldProduct.ProductMatreals.RemoveAll(x => !product.MatrealIds.Contains(x.MatrealId));
            foreach (var matrealId in product.MatrealIds.Where(x => !oldProduct.ProductMatreals.Any(bt => bt.MatrealId == x)))
            {
                ProductMatreal productMatreal = new ProductMatreal
                {
                    ProductId = product.Id,
                    MatrealId = matrealId
                };

                oldProduct.ProductMatreals.Add(productMatreal);
            }
            oldProduct.ProductColors.RemoveAll(x => !product.ColorIds.Contains(x.ColorId));
            foreach (var colorId in product.ColorIds.Where(x => !oldProduct.ProductColors.Any(bt => bt.ColorId == x)))
            {
                ProductColor productColor = new ProductColor
                {
                    ProductId = product.Id,
                    ColorId = colorId
                };

                oldProduct.ProductColors.Add(productColor);
            }
            
            oldProduct.Name = product.Name;
            oldProduct.SalePrice = product.SalePrice;
            oldProduct.CostPrice = product.CostPrice;
            oldProduct.DiscountPrice = product.DiscountPrice;
            oldProduct.IsNew = product.IsNew;
            oldProduct.Count = product.Count;
            oldProduct.Size = product.Size;
            oldProduct.DesignId = product.DesignId;
            oldProduct.Preference = product.Preference;
            oldProduct.Includes = product.Includes;
            oldProduct.IsInterestFree = product.IsInterestFree;
            oldProduct.SubCategoryId = product.SubCategoryId;
            _context.SaveChanges();
            
                return RedirectToAction("index");
            }
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return RedirectToAction("index", "error");
            }
            if (product.IsDeleted == false)
            {
                product.IsDeleted = true;
            }
            else
            {
                product.IsDeleted = false;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }


    }
}
