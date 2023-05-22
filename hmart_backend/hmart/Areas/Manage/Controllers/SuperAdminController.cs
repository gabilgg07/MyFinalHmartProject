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
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SuperAdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {

            List<AppUser> admins = _userManager.Users.Where(x => x.IsAdmin && x.Id != _userManager.GetUsersInRoleAsync("SuperAdmin").Result.FirstOrDefault().Id).ToList();

            return View(admins);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles.Where(x => x.Name != "Member" && x.Name != "SuperAdmin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateVM adminCreateVM)
        {
            ViewBag.Roles = _roleManager.Roles.Where(x => x.Name != "Member" && x.Name != "SuperAdmin");
            if (_userManager.Users.Any(x => x.UserName == adminCreateVM.UserName))
            {
                ModelState.AddModelError("UserName", "This username is using!");
                return View();
            }

            if (_userManager.Users.Any(x => x.Email == adminCreateVM.Email))
            {
                ModelState.AddModelError("Email", "This email is using!");
                return View();
            }

            if (adminCreateVM.Password != adminCreateVM.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "The password confirmation does not match!");
                return View();
            }

            AppUser admin = new AppUser()
            {
                FisrtName = adminCreateVM.FirstName,
                LastName = adminCreateVM.LastName,
                Email = adminCreateVM.Email,
                UserName = adminCreateVM.UserName,
                IsAdmin = true
            };

            await _userManager.CreateAsync(admin, adminCreateVM.Password);

            await _userManager.AddToRoleAsync(admin, adminCreateVM.Role);

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = _roleManager.Roles.Where(x => x.Name != "Member" && x.Name != "SuperAdmin");
            AppUser admin = await _userManager.FindByIdAsync(id);

            if (admin == null) return View("NotFoundPage");

            AdminEditVM adminEditVM = new AdminEditVM()
            {
                Id = admin.Id,
                FirstName = admin.FisrtName,
                LastName = admin.LastName,
                UserName = admin.UserName,
                Email = admin.Email,
                Role = _userManager.GetRolesAsync(admin).Result.FirstOrDefault()
            };

            return View(adminEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminEditVM adminEditVM)
        {
            ViewBag.Roles = _roleManager.Roles.Where(x => x.Name != "Member" && x.Name != "SuperAdmin");
            if (!ModelState.IsValid) return View();

            AppUser existAdmin = await _userManager.FindByIdAsync(adminEditVM.Id);

            if (existAdmin == null) return View("NotFoundPage");

            if (_userManager.Users.Any(x => x.UserName == adminEditVM.UserName) && existAdmin.UserName != adminEditVM.UserName)
            {
                ModelState.AddModelError("UserName", "This username is using!");
                return View(adminEditVM);
            }
            if (_userManager.Users.Any(x => x.Email == adminEditVM.Email) && existAdmin.Email != adminEditVM.Email)
            {
                ModelState.AddModelError("Email", "This email is using!");
                return View(adminEditVM);
            }

            if (adminEditVM.Password != null)
            {
                if (adminEditVM.Password != adminEditVM.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password confirmation does not match!");
                    return View(adminEditVM);
                }

                if (adminEditVM.OldPassword == null)
                {
                    ModelState.AddModelError("OldPassword", "Write your old password!");
                    return View(adminEditVM);
                }

                var result = await _userManager.ChangePasswordAsync(existAdmin, adminEditVM.OldPassword, adminEditVM.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View();
                    }
                }

            }

            existAdmin.FisrtName = adminEditVM.FirstName;
            existAdmin.LastName = adminEditVM.LastName;
            existAdmin.UserName = adminEditVM.UserName;
            existAdmin.Email = adminEditVM.Email;

            await _userManager.RemoveFromRoleAsync(existAdmin, _userManager.GetRolesAsync(existAdmin).Result.FirstOrDefault());

            await _userManager.AddToRoleAsync(existAdmin, adminEditVM.Role);

            await _userManager.UpdateAsync(existAdmin);

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.Roles = _roleManager.Roles.Where(x => x.Name != "Member" && x.Name != "SuperAdmin");

            AppUser admin = await _userManager.FindByIdAsync(id);

            if (admin == null) return View("NotFoundPage");

            AdminDeleteVM adminDeleteVM = new AdminDeleteVM()
            {
                Id = admin.Id,
                FirstName = admin.FisrtName,
                LastName = admin.LastName,
                UserName = admin.UserName,
                Email = admin.Email,
                Role = _userManager.GetRolesAsync(admin).Result.FirstOrDefault()
            };

            return View(adminDeleteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AdminDeleteVM adminDeleteVM)
        {
            AppUser admin = await _userManager.FindByIdAsync(adminDeleteVM.Id);

            if (admin == null) return View("NotFoundPage");

            await _userManager.DeleteAsync(admin);

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Set()
        {
            AppUser sadmin = await _userManager.FindByNameAsync(User.Identity.Name);

            AdminSetVM adminSetVM = new AdminSetVM
            {
                FirstName = sadmin.FisrtName,
                LastName = sadmin.LastName,
                UserName = sadmin.UserName,
                Email = sadmin.Email
            };

            return View(adminSetVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(AdminSetVM adminSetVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser sadmin = await _userManager.FindByNameAsync(User.Identity.Name);

            if (_userManager.Users.Any(x => x.UserName == adminSetVM.UserName) && sadmin.UserName != adminSetVM.UserName)
            {
                ModelState.AddModelError("UserName", "This username is using!");
                return View(adminSetVM);
            }
            if (_userManager.Users.Any(x => x.Email == adminSetVM.Email) && sadmin.Email != adminSetVM.Email)
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

                var result = await _userManager.ChangePasswordAsync(sadmin, adminSetVM.OldPassword, adminSetVM.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View();
                    }
                }

            }

            sadmin.FisrtName = adminSetVM.FirstName;
            sadmin.LastName = adminSetVM.LastName;
            sadmin.UserName = adminSetVM.UserName;
            sadmin.Email = adminSetVM.Email;

            await _userManager.UpdateAsync(sadmin);

            return RedirectToAction("index", "dashboard");
        }
    }
}
