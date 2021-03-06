using Madeyra.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
    public class CompanyController : Controller
    {
        private readonly MContext _context;
        public CompanyController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Campagains.ToList());
        }
    }
}
