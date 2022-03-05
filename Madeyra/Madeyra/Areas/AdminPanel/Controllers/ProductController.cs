using Madeyra.Helpers;
using Madeyra.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        MContext _context;
        IWebHostEnvironment _env;
        public ProductController(MContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            return View(_context.Products.Include(x=>x.ProductMatreals).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.SubCategory = _context.SubCategories.ToList();
            ViewBag.ColorIds = _context.Colors.ToList();
            ViewBag.DesignId = _context.Designs.ToList();
            ViewBag.MatrealId = _context.Matreals.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.SubCategory = _context.SubCategories.ToList();
            ViewBag.ColorIds = _context.Colors.ToList();
            ViewBag.DesignId = _context.Designs.ToList();
            ViewBag.MatrealId = _context.Matreals.ToList();
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
            if(product.Price<0)
            {
                ModelState.AddModelError("Price","Qiymət mənfi ola bilməz")
            }
            if (!ModelState.IsValid) return View();
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
       /* public IActionResult Update(int id)
        {
            Product product = _context.Products.Include(x => x.ProductMatreals).Include(x => x.ProductColors).FirstOrDefault(x => x.Id == id);
            if(product==null)
            {
                return RedirectToAction("index", "error");
            }
            ViewBag.SubCategory = _context.SubCategories.ToList();
            ViewBag.ColorIds = _context.Colors.ToList();
            ViewBag.DesignId = _context.Designs.ToList();
            ViewBag.MatrealId = _context.Matreals.ToList();
            product.MatrealIds = product.ProductMatreals.Select(x => x.MatrealId).ToList();
            product.ColorIds = product.ProductColors.Select(x => x.ColorId).ToList();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            ViewBag.SubCategory = _context.SubCategories.ToList();
            ViewBag.ColorIds = _context.Colors.ToList();
            ViewBag.DesignId = _context.Designs.ToList();
            ViewBag.MatrealId = _context.Matreals.ToList();
            return View();
        }*/
    }
}
