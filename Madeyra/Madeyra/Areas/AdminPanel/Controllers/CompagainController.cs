using Madeyra.Helpers;
using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CompagainController : Controller
    {
        private readonly MContext _context;
        private readonly IWebHostEnvironment _env;
        public CompagainController(MContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Campagains.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Campagain campagain)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if(campagain.FormFile==null)
            {
                ModelState.AddModelError("FormFile", "Şəkil məcburidir!");
                return View();
            }
            if(campagain.StartTime==null || campagain.EndTime==null)
            {
                ModelState.AddModelError("", "Tarixlər qeyd edilməlidir!");
                return View();
            }
            if(_env.WebRootPath == null)
            {
                _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            if (campagain.FormFile!=null)
            {
                
                if (campagain.FormFile.ContentType != "image/jpeg" && campagain.FormFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                    return View();
                }
                if (campagain.FormFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                    return View();
                }
                campagain.Image = FileManager.Save(_env.WebRootPath, "uploads/campagain", campagain.FormFile);

            }
            campagain.StartTime =DateTime.UtcNow.AddHours(4);
            _context.Campagains.Add(campagain);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Campagain old = _context.Campagains.FirstOrDefault(x => x.Id == id);
            if(old==null)
            {
                return RedirectToAction("index", "error");
            }
            return View(old);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Campagain campagain)
        {
            Campagain old = _context.Campagains.FirstOrDefault(x => x.Id == campagain.Id);
            if (campagain.FormFile != null)
            {
                if (campagain.FormFile.ContentType != "image/jpeg" && campagain.FormFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                    return View();
                }
                if (campagain.FormFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                    return View();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/campagain", old.Image);
                campagain.Image = FileManager.Save(_env.WebRootPath, "uploads/campagain", campagain.FormFile);
                old.Image = campagain.Image;
            }
            old.Title = campagain.Title;
            old.EndTime = campagain.EndTime;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Campagain old = _context.Campagains.FirstOrDefault(x => x.Id == id);
            if (old == null)
            {
                return RedirectToAction("index", "error");
            }
            FileManager.Delete(_env.WebRootPath, "uploads/campagain", old.Image);
            _context.Campagains.Remove(old);
            _context.SaveChanges();
            return View("index");
        }
    }
}
