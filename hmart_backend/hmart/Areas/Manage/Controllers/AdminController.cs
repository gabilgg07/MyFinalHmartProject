using hmart.Areas.Manage.ViewModels;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,EditorAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Set()
        {
            AppUser admin = await _userManager.FindByNameAsync(User.Identity.Name);

            AdminSetVM adminSetVM = new AdminSetVM
            {
                FirstName = admin.FisrtName,
                LastName = admin.LastName,
                UserName = admin.UserName,
                Email = admin.Email
            };

            return View(adminSetVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(AdminSetVM adminSetVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser admin = await _userManager.FindByNameAsync(User.Identity.Name);

            if (_userManager.Users.Any(x => x.UserName == adminSetVM.UserName) && admin.UserName != adminSetVM.UserName)
            {
                ModelState.AddModelError("UserName", "This username is using!");
                return View(adminSetVM);
            }
            if (_userManager.Users.Any(x => x.Email == adminSetVM.Email) && admin.Email != adminSetVM.Email)
            {
                ModelState.AddModelError("Email", "This email is using!");
                return View(adminSetVM);
            }

            if (adminSetVM.Password != null)
            {
                if (adminSetVM.Password != adminSetVM.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password confirmation does not match!");
                    return View(adminSetVM);
                }

                if (adminSetVM.OldPassword == null)
                {
                    ModelState.AddModelError("OldPassword", "Write your old password!");
                    return View(adminSetVM);
                }

                var result = await _userManager.ChangePasswordAsync(admin, adminSetVM.OldPassword, adminSetVM.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View();
                    }
                }

            }

            admin.FisrtName = adminSetVM.FirstName;
            admin.LastName = adminSetVM.LastName;
            admin.UserName = adminSetVM.UserName;
            admin.Email = adminSetVM.Email;

            await _userManager.UpdateAsync(admin);

            return RedirectToAction("index", "dashboard");
        }
    }
}
