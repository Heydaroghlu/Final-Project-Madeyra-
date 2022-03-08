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
    [Authorize(Roles="Admin,SuperAdmin")]
    public class DesignController : Controller
    {
        MContext _context;
        public DesignController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? deleted = null, string? search = null)
        {
            var design = _context.Designs.AsQueryable();


            if (deleted == true)
            {
                design = design.Where(x => x.IsDeleted == true);
            }
            if (search != null)
            {
                design = design.Where(x => x.Name.Contains(search));
            }

            ViewBag.IsDeleted = deleted;
            ViewBag.Search = search;
            return View(design.ToPagedList(page, 10));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Design design)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Design olddesign = _context.Designs.FirstOrDefault(x => x.Name == design.Name);
            if (olddesign != null)
            {
                ModelState.AddModelError("Name", "Bu ad da Rəng mövcutdur!");
                return View();
            }
            _context.Designs.Add(design);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Design design = _context.Designs.FirstOrDefault(x => x.Id == id);
            if (design == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(design);
        }
        [HttpPost]
        public IActionResult Update(Design design)
        {
            Design olddesign = _context.Designs.FirstOrDefault(x => x.Id == design.Id);
            if (olddesign == null)
            {
                return RedirectToAction("index", "error");
            }
            olddesign.Name = design.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Design design = _context.Designs.FirstOrDefault(x => x.Id == id);
            if (design == null)
            {
                return RedirectToAction("index", "error");
            }
            if (design.IsDeleted == false)
            {
                design.IsDeleted = true;
            }
            else
            {
                design.IsDeleted = false;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
