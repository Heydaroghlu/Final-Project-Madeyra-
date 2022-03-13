using Madeyra.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class VideoController : Controller
    {
        private readonly MContext _context;
        public VideoController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Videos.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Video video)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            _context.Videos.Add(video);
            _context.SaveChanges();
            return View();
        }
        public IActionResult Update(int id)
        {
            Video oldvideo = _context.Videos.FirstOrDefault(x => x.Id == id);
            if(oldvideo==null)
            {
                return RedirectToAction("index", "error");
            }
            return View(oldvideo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Video video)
        {
            Video oldvideo = _context.Videos.FirstOrDefault(x => x.Id == video.Id);
            if (oldvideo == null)
            {
                return RedirectToAction("index", "error");
            }
            oldvideo.VideoUrl = video.VideoUrl;
            _context.SaveChanges();
            return View("index");
        }
        public IActionResult Delete(int id)
        {
            Video oldvideo = _context.Videos.FirstOrDefault(x => x.Id == id);
            if (oldvideo == null)
            {
                return RedirectToAction("index", "error");
            }
            _context.Videos.Remove(oldvideo);
            _context.SaveChanges();
            return View("index");
        }
    }
}
