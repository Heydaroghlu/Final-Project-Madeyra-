﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
