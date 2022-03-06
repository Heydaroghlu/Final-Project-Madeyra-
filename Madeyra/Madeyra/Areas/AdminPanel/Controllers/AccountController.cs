
using Madeyra.Areas.AdminPanel.ViewModels;
using Madeyra.Models;
using Madeyra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly MContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, MContext context)
        {
            _emailService = emailService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLogin)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == adminLogin.Username.ToUpper() && x.IsAdmin==true);
            if(admin==null)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır!!!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLogin.Password, false, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır!!!");
                return View();
            }
            return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login","account");
        }
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Forgot(ForgotViewModel forgotView)
        {
            if(!ModelState.IsValid)
            { return View(); }
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == forgotView.Email.ToUpper());
           if(admin==null)
            {
                ModelState.AddModelError("Email", "Bele bir istifadeci yoxdur!!!");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(admin);
            var url = Url.Action("resetpassword", "account", new { email = admin.Email, token }, Request.Scheme);
            _emailService.Send(admin.Email, "Şifrə Dəyişikliyi", "<a href='" + url + "'>Şifrə Dəyişikliyi</a>");
            return RedirectToAction("login", "account");
        }
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            AppUser user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null || !(await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPassword.Token)))
            {
                return RedirectToAction("index","error");
            }

            return View(resetPassword);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View("ResetPassword", resetPassword);
            }
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == resetPassword.Email.ToUpper());
            if (user == null)
            {
                ModelState.AddModelError("Email", "Bele bir istifadeci yoxdur!!!");
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("ResetPassword", resetPassword);
            }
            return RedirectToAction("login");

        }
        public IActionResult Admins()
        {
        
           if(!User.IsInRole("SuperAdmin"))
            {
                return RedirectToAction("index","error");
            }
            return View(_context.AppUsers.ToList());
        }
        [Authorize(Roles ="SuperAdmin")]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AdminViewModel adminView)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == adminView.Email);
            if(admin!=null)
            {
                ModelState.AddModelError("Email", "Bu Email hal hazırda istifadə olunur");
                return View();
            }
             admin = new AppUser
            {
                Name = adminView.Name,
                Surname = adminView.Surname,
                Email = adminView.Email,
                UserName = adminView.Email,
                PhoneNumber=adminView.PhoneNumber,
                IsAdmin = true
            };
            var result = await _userManager.CreateAsync(admin, adminView.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(admin, "Admin");
            return RedirectToAction("index", "dashboard");
        }
       /* public async Task<IActionResult> CreateSuperAdmin()
        {
            AppUser superadmin = new AppUser
            {
                UserName = "SuperAdmin",
                IsAdmin = true,
                Email="tu201906065",
                PhoneNumber="0777100910"
            };
            
            var result = await _userManager.CreateAsync(superadmin, "Admin123");
            await _userManager.AddToRoleAsync(superadmin, "SuperAdmin");
            return Ok(result);
        }*/
    }
}
