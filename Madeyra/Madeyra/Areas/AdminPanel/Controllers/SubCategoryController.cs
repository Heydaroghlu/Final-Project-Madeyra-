using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SubCategoryController : Controller
    {
        MContext _context;
        public SubCategoryController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? deleted = null, string? search = null)
        {
            var categories = _context.SubCategories.AsQueryable();

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
            return View(categories.Include(x=>x.Category).ToPagedList(page, 5));
        }
        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.Where(x => x.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Category = _context.Categories.Where(x=>x.IsDeleted==false).ToList();
            Category existscategory = _context.Categories.FirstOrDefault(x => x.Name.ToUpper() == category.Name.ToUpper());
            if (existscategory != null)
            {
                ModelState.AddModelError("Name", "Bu ad da Kateqoriya Mövcutdur!");
                return View();
            }
            _context.SubCategories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Category = _context.Categories.ToList();
            SubCategory oldcategory = _context.SubCategories.FirstOrDefault(x => x.Id == id);
            if (oldcategory == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(oldcategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SubCategory category)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            SubCategory oldcategory = _context.SubCategories.FirstOrDefault(x => x.Id == category.Id);
            if (oldcategory == null)
            {
                return RedirectToAction("index", "error");
            }
            oldcategory.Name = category.Name;
            oldcategory.CategoryId = category.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            SubCategory category = _context.SubCategories.FirstOrDefault(x => x.Id == id);
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
