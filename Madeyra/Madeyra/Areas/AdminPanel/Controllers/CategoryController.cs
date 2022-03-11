using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly MContext _context;
        public CategoryController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? deleted = null, string? search = null)
        {
            var categories = _context.Categories.AsQueryable();

            if (deleted == true)
            {
                categories = categories.Where(x => x.IsDeleted == true);
            }
            if (search != null)
            {
                categories = categories.Where(x => x.Name.Contains(search));
            }

            ViewBag.IsDeleted = deleted;
            ViewBag.Search = search;
            return View(categories.ToPagedList(page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Category existscategory = _context.Categories.FirstOrDefault(x => x.Name.ToUpper() == category.Name.ToUpper());
            if(existscategory!=null)
            {
                ModelState.AddModelError("Name", "Bu ad da Kateqoriya Mövcutdur!");
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index","category");
        }
        public IActionResult Update(int id)
        {
            Category oldcategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (oldcategory == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(oldcategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Category oldcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if(oldcategory==null)
            {
                return RedirectToAction("index", "error");
            }
            oldcategory.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction("index","category");
        }
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("index", "error");
            }
            if (category.IsDeleted == false)
            {
                category.IsDeleted = true;
            }
            else
            {
                category.IsDeleted = false;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
