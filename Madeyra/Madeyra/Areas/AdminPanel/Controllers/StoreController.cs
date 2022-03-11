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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class StoreController : Controller
    {
        private readonly MContext _context;
        public StoreController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            return View(_context.Stores.ToPagedList(page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Store store)
        {
            Store existsStore = _context.Stores.FirstOrDefault(x => x.Address == store.Address);
            if (existsStore != null)
            {
                ModelState.AddModelError("Name", "Bu Mağaza Databazada mövcutdur!");
                return View();
            }
            _context.Stores.Add(store);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Store store = _context.Stores.FirstOrDefault(x => x.Id == id);
            if (store == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(store);
        }
        [HttpPost]
        public IActionResult Update(Store store)
        {
            Store oldstore = _context.Stores.FirstOrDefault(x => x.Id == store.Id);
            if (oldstore == null)
            {
                return RedirectToAction("index", "error");
            }
            oldstore.Name = store.Name;
            oldstore.Address = store.Address;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Store store = _context.Stores.FirstOrDefault(x => x.Id == id);
            if (store == null)
            {
                return RedirectToAction("index", "error");
            }
            _context.Stores.Remove(store);
            _context.SaveChanges();
            return View("index");
        }
    }
}
