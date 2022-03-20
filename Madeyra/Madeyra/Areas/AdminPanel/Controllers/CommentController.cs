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
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CommentController : Controller
    {
        private readonly MContext _context;
        public CommentController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int deleted = 0, string? search = null)
        {
            var comments = _context.ProductComments.Include(x => x.AppUser).Include(x => x.Product).AsQueryable();
            if (deleted == 0)
            {
                comments = comments;
            }
            if (deleted == 1)
            {
                comments = comments.Where(x => x.Status==true);
            }
            else if (deleted == 2)
            {
                comments = comments.Where(x => x.Status == false);
            }

            if (search != null)
            {
                comments = comments.Where(x => x.Name.Contains(search) || x.AppUser.Name.Contains(search) || x.AppUser.Surname.Contains(search) || x.Product.Name.Contains(search));
            }
            return View(comments.ToPagedList(page,10));
        }
        public IActionResult Status(int id)
        {
            ProductComment comment = _context.ProductComments.FirstOrDefault(x => x.Id == id);
            if(comment==null)
            {
                return View("index", "error");
            }
            if(comment.Status==true)
            {
                comment.Status = false;
            }
            else
            {
                comment.Status = true;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
