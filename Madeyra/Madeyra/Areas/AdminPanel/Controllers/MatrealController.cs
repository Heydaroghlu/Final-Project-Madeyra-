using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Index()
        {
            return View(_context.Matreals.ToList());
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
    }
}
