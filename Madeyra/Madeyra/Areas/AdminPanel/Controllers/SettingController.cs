using Madeyra.Helpers;
using Madeyra.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SettingController : Controller
    {
        private readonly MContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingController(MContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Settings.ToList());
        }
        public IActionResult Update()
        {
            return View(_context.Settings.FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Update(Setting setting)
        {
            Setting oldSetting = _context.Settings.FirstOrDefault();
            if(oldSetting==null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return View(oldSetting);
            if(setting.HeaderLogoFile!=null)
            {
                if (setting.HeaderLogoFile.ContentType!="image/jpeg" && setting.HeaderLogoFile.ContentType!="image/png")
                {
                    ModelState.AddModelError("HeaderLogoFile","Şəkilin formatı düzgün deyil !");
                    return View();
                }
                if(setting.HeaderLogoFile.Length>2097152)
                {
                    ModelState.AddModelError("HeaderLogoFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                    return View();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/setting", oldSetting.HeaderLogo);
                oldSetting.HeaderLogo = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.HeaderLogoFile); ;
            }
            if (setting.FooterLogoFile != null)
            {
                if (setting.FooterLogoFile.ContentType != "image/jpeg" && setting.FooterLogoFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("HeaderLogoFile", "Şəkilin formatı düzgün deyil !");
                    return View();
                }
                if (setting.FooterLogoFile.Length > 2097152)
                {
                    ModelState.AddModelError("HeaderLogoFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                    return View();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/setting", oldSetting.FooterLogo);
                oldSetting.FooterLogo = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.FooterLogoFile); ;
            }
            oldSetting.FacebookIcon = setting.FacebookIcon;
            oldSetting.FacebookUrl = setting.FacebookUrl;
            oldSetting.InstagramIcon = setting.InstagramIcon;
            oldSetting.Tel = setting.Tel;
            oldSetting.TelIcon = setting.TelIcon;
            oldSetting.WhatsappIcon = setting.WhatsappIcon;
            oldSetting.WhatsappUrl = setting.WhatsappUrl;
            oldSetting.Adress = setting.Adress;
            oldSetting.Faks = setting.Faks;
            oldSetting.YoutubeIcon = setting.YoutubeIcon;
            oldSetting.YoutubeUrl = setting.YoutubeUrl;
            oldSetting.Email = setting.Email;
            _context.SaveChanges();
            return RedirectToAction("index", "setting");
        }
    }
}
