using hmart.Areas.Manage.ViewModels;
using hmart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginVM adminLoginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser admin = await _userManager.FindByNameAsync(adminLoginVM.UserName);

            if (admin == null || !admin.IsAdmin)
            {
                ModelState.AddModelError("", "Username or Password is false!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginVM.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is false!");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }
    }
}
