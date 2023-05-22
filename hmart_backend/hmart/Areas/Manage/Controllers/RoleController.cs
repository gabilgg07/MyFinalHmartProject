using hmart.Areas.Manage.ViewModels;
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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.Where(x => x.Name != "Member" && x.Name != "SuperAdmin");

            List<RoleVM> roleVMs = new List<RoleVM>();

            foreach (var item in roles)
            {
                RoleVM roleVM = new RoleVM
                {
                    Id = item.Id,
                    Name = item.Name
                };
                roleVMs.Add(roleVM);
            }

            return View(roleVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateVM roleCreateVM)
        {

            await _roleManager.CreateAsync(new IdentityRole { Name = roleCreateVM.Name });

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return View("NotFoundPage");

            RoleVM roleVM = new RoleVM
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleVM roleVM)
        {
            var role = await _roleManager.FindByIdAsync(roleVM.Id);

            if (role == null) return View("NotFoundPage");

            if (await _roleManager.FindByNameAsync(roleVM.Name) != null && role.Name !=roleVM.Name)
            {
                ModelState.AddModelError("Name", "This name is using!");
                return View(roleVM);
            }

            role.Name = roleVM.Name;

            await _roleManager.UpdateAsync(role);

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return View("NotFoundPage");

            RoleVM roleVM = new RoleVM
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleVM roleVM)
        {
            var role = await _roleManager.FindByIdAsync(roleVM.Id);

            if (role == null) return View("NotFoundPage");

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("index");
        }
    }
}
