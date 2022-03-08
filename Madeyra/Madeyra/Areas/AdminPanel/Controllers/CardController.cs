using Madeyra.Helpers;
using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class CardController : Controller
    {
        MContext _context;
        IWebHostEnvironment _env;
        public CardController(MContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            return View(_context.Cards.ToPagedList(page,4));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Card card)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (card.Text==null)
            {
                ModelState.AddModelError("Text", "Text Boş ola bilməz!");
                return View();
            }
            if(card.CardImageFile!=null)
            {
                if(card.CardImageFile.ContentType!="image/png" && card.CardImageFile.ContentType!="image/jpeg")
                {
                    ModelState.AddModelError("CardImageFile", "Şəklin formatı düzgün deyil!");
                        return View();
                }
                if(card.CardImageFile.Length>2097152)
                {
                    ModelState.AddModelError("CardImageFile", "Şəklin ölçüsü max 2 mb!");
                    return View();
                }
               
                card.Image = FileManager.Save(_env.WebRootPath, "uploads/card", card.CardImageFile);
            }
            else
            {
                ModelState.AddModelError("CardImageFile", "Şəkl boş ola bilməz");
                return View();
            }
            _context.Cards.Add(card);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Card card = _context.Cards.FirstOrDefault(x => x.Id == id);
            if(card==null)
            {
                return RedirectToAction("index", "error");
            }
            return View(card);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Card card)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Card oldcard = _context.Cards.FirstOrDefault(x => x.Id == card.Id);
            if (card == null)
            {
                return RedirectToAction("index", "error");
            }
            if (card.CardImageFile != null)
            {
                if (card.CardImageFile.ContentType != "image/png" && card.CardImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("CardImageFile", "Şəklin formatı düzgün deyil!");
                    return View();
                }
                if (card.CardImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("CardImageFile", "Şəklin ölçüsü max 2 mb!");
                    return View();
                }
                 
                FileManager.Delete(_env.WebRootPath, "uploads/card", oldcard.Image);
                oldcard.Image = FileManager.Save(_env.WebRootPath, "uploads/card", card.CardImageFile); ;

            }
            else
            {
                ModelState.AddModelError("CardImageFile", "Şəkl boş ola bilməz");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Card card = _context.Cards.FirstOrDefault(x => x.Id == id);
            if (card == null)
            {
                return RedirectToAction("index", "error");
            }
            FileManager.Delete(_env.WebRootPath, "uploads/card", card.Image);
            _context.Cards.Remove(card);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
