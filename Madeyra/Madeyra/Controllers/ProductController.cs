using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
    public class ProductController : Controller
    {
        MContext _context;
        public ProductController(MContext context)
        {
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
    }
}
