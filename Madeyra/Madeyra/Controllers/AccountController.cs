using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public AccountController(MContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
                ModelState.AddModelError("Email", "This Email is already taken!");
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
            AppUser member =  _userManager.Users.FirstOrDefault(x => x.NormalizedEmail == memberLogin.Email.ToUpper() && x.IsAdmin==false);
            if(member==null)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(member, memberLogin.Password, memberLogin.IsPersistent, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> CreateRoles()
        {
            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(role3);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role1);
            return Ok();
        }
       
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
            if(!string.IsNullOrWhiteSpace(profile.NewPassword) && !string.IsNullOrWhiteSpace(profile.NewPassword) && profile.NewPassword==profile.NewPasswordConfirmed)
            {
                var passwordchange = await _userManager.ChangePasswordAsync(oldprofile, profile.CurrentPassword, profile.NewPassword);
                if(!passwordchange.Succeeded)
                {
                    foreach(var error in passwordchange.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
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
    }
}
