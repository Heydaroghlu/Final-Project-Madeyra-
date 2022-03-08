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
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class ColorController : Controller
    {
        MContext _context;
        public ColorController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? deleted = null, string? search = null)
        {
            var color = _context.Colors.AsQueryable();
            

            if (deleted == true)
            {
                color = color.Where(x => x.IsDeleted == true);
            }
            if (search != null)
            {
                color = color.Where(x => x.Name.Contains(search));
            }

            ViewBag.IsDeleted = deleted;
            ViewBag.Search = search;
            return View(color.ToPagedList(page, 10));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Color oldcolor = _context.Colors.FirstOrDefault(x => x.Name == color.Name);
            if (oldcolor != null)
            {
                ModelState.AddModelError("Name", "Bu ad da Rəng mövcutdur!");
                return View();
            }
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(color);
        }
        [HttpPost]
        public IActionResult Update(Color color)
        {
            Color oldcolor = _context.Colors.FirstOrDefault(x => x.Id == color.Id);
            if (color == null)
            {
                return RedirectToAction("index", "error");
            }
            oldcolor.Name = color.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null)
            {
                return RedirectToAction("index", "error");
            }
            if (color.IsDeleted == false)
            {
                color.IsDeleted = true;
            }
            else
            {
                color.IsDeleted = false;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
