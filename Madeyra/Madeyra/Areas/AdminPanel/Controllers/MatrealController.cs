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
    public class MatrealController : Controller
    {
        MContext _context;
        public MatrealController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, bool? deleted = null, string? search = null)
        {
            var matreal = _context.Matreals.AsQueryable();

            if (deleted == true)
            {
                matreal = matreal.Where(x => x.IsDeleted == true);
            }
            if (search != null)
            {
                matreal = matreal.Where(x => x.Name.Contains(search));
            }

            ViewBag.IsDeleted = deleted;
            ViewBag.Search = search;
            return View(matreal.ToPagedList(page,10));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Matreal matreal)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Matreal oldmatreal = _context.Matreals.FirstOrDefault(x => x.Name == matreal.Name);
            if(oldmatreal!=null)
            {
                ModelState.AddModelError("Name", "Bu ad da matreal mövcutdur!");
                return View();
            }
            _context.Matreals.Add(matreal);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Matreal matreal = _context.Matreals.FirstOrDefault(x => x.Id == id);
            if(matreal==null)
            {
                return RedirectToAction("index", "error");
            }
            return View(matreal);
        }
        [HttpPost]
        public IActionResult Update(Matreal matreal)
        {
            Matreal oldmatreal = _context.Matreals.FirstOrDefault(x => x.Id == matreal.Id);
            if (matreal == null)
            {
                return RedirectToAction("index", "error");
            }
            oldmatreal.Name = matreal.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Matreal matreal = _context.Matreals.FirstOrDefault(x => x.Id == id);
            if (matreal == null)
            {
                return RedirectToAction("index", "error");
            }
            if(matreal.IsDeleted==false)
            {
                matreal.IsDeleted = true;
            }
            else
            {
                matreal.IsDeleted = false;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
