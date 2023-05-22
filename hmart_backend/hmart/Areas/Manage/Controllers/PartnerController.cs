using hmart.DAL;
using hmart.Helpers;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,EditorAdmin")]
    public class PartnerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PartnerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Partner> partners = _context.Partners.ToList();

            return View(partners);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Partner partner)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Partners.Any(x => x.Order == partner.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            if (partner.ImageFile != null)
            {
                if (partner.ImageFile.ContentType != "image/jpeg" && partner.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                    return View();
                }

                if (partner.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only maximum 5Mb !");
                    return View();
                }

                partner.Image = FileManager.Save(_env.WebRootPath, "uploads/partners", partner.ImageFile);
            }

            _context.Partners.Add(partner);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/partners", partner.Image);
            }

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Partner partner = _context.Partners.FirstOrDefault(x => x.Id == id);

            if (partner == null) return View("NotFoundPage");

            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Partner prt)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Partners.Any(x => x.Order == prt.Order && x.Id != prt.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Partner partner = _context.Partners.FirstOrDefault(x => x.Id == prt.Id);

            if (partner == null) return View("NotFoundPage");

            if (prt.ImageFile != null)
            {
                if (prt.ImageFile.ContentType != "image/jpeg" && prt.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (prt.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/partners", prt.ImageFile);

                if (!string.IsNullOrWhiteSpace(partner.Image))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/partners", partner.Image);
                }

                partner.Image = newFileName;
            }
            else if (string.IsNullOrWhiteSpace(prt.Image) && !string.IsNullOrWhiteSpace(partner.Image))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/partners", partner.Image);

                partner.Image = null;
            }

            partner.Order = prt.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Partner partner = _context.Partners.FirstOrDefault(x => x.Id == id);

            if (partner == null) return View("NotFoundPage");

            return View(partner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Partner prt)
        {
            Partner partner = _context.Partners.FirstOrDefault(x => x.Id == prt.Id);

            if (partner == null) return View("NotFoundPage");

            if (!string.IsNullOrWhiteSpace(partner.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/partners", partner.Image);
            }

            _context.Partners.Remove(partner);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
