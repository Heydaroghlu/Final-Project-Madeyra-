using Madeyra.Helpers;
using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class SlideController : Controller
    {
        private readonly MContext _context;
        private readonly IWebHostEnvironment _env;
        public SlideController(MContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                    return View();
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                    return View();
                }
                slider.Image = FileManager.Save(_env.WebRootPath, "uploads/slider", slider.ImageFile); 
            }
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)

        {
            Slider old = _context.Sliders.FirstOrDefault(x => x.Id == id);
            return View(old);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Slider slider)
        {
            Slider old = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if(slider==null)
            {
                return RedirectToAction("index", "error");
            }
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                    return View();
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                    return View();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/setting", old.Image);
                slider.Image = FileManager.Save(_env.WebRootPath, "uploads/slider", slider.ImageFile);
                old.Image = slider.Image;
                _context.SaveChanges();
            }
            return RedirectToAction("index");

        }
        public IActionResult Delete(int id)
        {
            Slider old = _context.Sliders.FirstOrDefault(x => x.Id == id);
          if(old==null)
            {
                return RedirectToAction("index","error");
            }
            FileManager.Delete(_env.WebRootPath, "uploads/slider", old.Image);
            _context.Sliders.Remove(old);
            _context.SaveChanges();
            return View("index");
        }
    }
}
