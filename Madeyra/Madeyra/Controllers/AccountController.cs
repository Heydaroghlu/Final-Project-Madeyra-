using Madeyra.Models;
using Madeyra.Services;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Controllers
{
    public class AccountController : Controller
    {
        private readonly MContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        public AccountController(MContext context,IEmailService emailService,IWebHostEnvironment env,RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _context = context;
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegister)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
           
            if(_context.AppUsers.Any(x=>x.NormalizedEmail==memberRegister.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Bu Emaili istifadə edə bilməzsiniz!");
                return View();
            }
            AppUser member = new AppUser
            {
                Email = memberRegister.Email,
                UserName = memberRegister.Email,
                Name=memberRegister.Name,
                Surname=memberRegister.Surname,
                PhoneNumber=memberRegister.Telephone,
                Adress=memberRegister.Adress,
                IsAdmin = false
                
            };
            var result =await _userManager.CreateAsync(member, memberRegister.Password);
            if(!result.Succeeded)
            {
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(member, "Member");
            await _signInManager.SignInAsync(member, true);
            return RedirectToAction("index", "home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(MemberLoginViewModel memberLogin)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (memberLogin.Password == null || memberLogin.Email == null)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır");
                return View();
            }
            AppUser member =  _userManager.Users.FirstOrDefault(x => x.NormalizedEmail == memberLogin.Email.ToUpper() && x.IsAdmin==false);
            if(member==null)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır");
                return View();
            }
            
            var result = await _signInManager.PasswordSignInAsync(member, memberLogin.Password, memberLogin.IsPersistent, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır");
                return View();
            }
            BasketViewModel basketView = null;
            List<BasketViewModel> basketlist = new List<BasketViewModel>();
            string basketStr;
            if(HttpContext.Request.Cookies["Basket"]!=null)
            {
                basketStr = HttpContext.Request.Cookies["Basket"];
                basketlist = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketStr);
            }
            foreach(var item in basketlist)
            {
                BasketItem newbasket = new BasketItem
                {
                    AppUserId = member.Id,
                    ProductId = item.ProductId,
                    Count = 1,
                    Price = item.Price,
                    Discount = item.Discount
                };
                _context.BasketItems.Add(newbasket);
            }
            _context.SaveChanges();

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
     /*   public async Task<IActionResult> CreateRoles()
        {
            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(role3);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role1);
            return Ok();
        }*/
        [Authorize(Roles ="Member")]

        public IActionResult ProfileMenu()
        {
            return View();
        }
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Profile()
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileViewModel profile = new ProfileViewModel
            {
                Name = member.Name,
                Surname = member.Surname,
                PhoneNumber = member.PhoneNumber,
                Adress = member.Adress,
                Email = member.Email
            };

            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Profile(ProfileViewModel profile)
        {
            AppUser oldprofile = await _userManager.FindByNameAsync(User.Identity.Name);
            if(oldprofile==null)
            {
                return NotFound();
            }
            oldprofile.Name = profile.Name;
            oldprofile.Surname = profile.Surname;
            oldprofile.PhoneNumber = profile.PhoneNumber;
            oldprofile.Adress = profile.Adress;
            var result = await _userManager.UpdateAsync(oldprofile);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            if (!string.IsNullOrWhiteSpace(profile.NewPassword) && !string.IsNullOrWhiteSpace(profile.NewPassword) && profile.NewPassword == profile.NewPasswordConfirmed)
            {
                var passwordchange = await _userManager.ChangePasswordAsync(oldprofile, profile.CurrentPassword, profile.NewPassword);
                if (!passwordchange.Succeeded)
                {
                    foreach (var error in passwordchange.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
            }
            return RedirectToAction("profilemenu", "account");
        }
        public async Task<IActionResult>  Adress()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forgot(ForgotViewModel forgotView)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(forgotView.Email);
            if(user==null)
            {
                ModelState.AddModelError("Email", "Belə bir istifadəçi Mövcud deyil!!!");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("resetpassword", "account", new { email = user.Email, token }, Request.Scheme);
            var passurl = Url.Action("index", "home");
            var path = _env.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Other"
                + Path.DirectorySeparatorChar.ToString() + "htmlpage.html";
            var builder = new BodyBuilder();

            using(StreamReader streamReader=System.IO.File.OpenText(path))
            {
                builder.HtmlBody = streamReader.ReadToEnd();
            }
            string message = string.Format(builder.HtmlBody, url, passurl);
            _emailService.Send(user.Email, "Yeni Şifrə", message);

            return RedirectToAction("index","home");
        }
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if(resetPassword.Email==null)
            {
                return RedirectToAction("index", "error");
            }
            AppUser user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null || !(await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPassword.Token)))
            {
                return RedirectToAction("index", "error");
            }

            return View(resetPassword);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Member")]

        public async Task<IActionResult> ChangePassword(ResetPasswordViewModel resetPassword)
        {
            if(!ModelState.IsValid)
            {
                return View("ResetPassword",resetPassword);
            }
            if(resetPassword.Password==null || resetPassword.ConfirmPassword==null)
            {
                ModelState.AddModelError("", "Hər iki xana düzgün doldurulmalıdır!");
                return View("ResetPassword", resetPassword);
            }
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == resetPassword.Email.ToUpper());
            if (user == null)
            {
                ModelState.AddModelError("Email", "Bele bir istifadeci yoxdur!");
                return View("ResetPassword", resetPassword);
            }
            var result = await _userManager.ResetPasswordAsync(user,resetPassword.Token, resetPassword.Password);
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
        [Authorize(Roles ="Member")]
        public IActionResult Orders()
        {
            AppUser member = null;
            
            if(User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.NormalizedUserName == User.Identity.Name.ToUpper());
            }
            List<Order> Orders = _context.Orders.Include(x => x.OrderItems)
                .ThenInclude(x=>x.Product)
                .ThenInclude(x=>x.ProductImages)
                .Where(x => x.AppUserId == member.Id).ToList();
            return View(Orders);
        }
    }
}
