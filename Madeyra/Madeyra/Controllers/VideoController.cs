using Madeyra.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
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
    }
}
