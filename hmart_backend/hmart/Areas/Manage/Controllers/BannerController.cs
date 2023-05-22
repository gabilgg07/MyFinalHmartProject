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
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Banner> banners = _context.Banners.ToList();

            return View(banners);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Banner banner)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Banners.Any(x => x.Order == banner.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            if (banner.ImageFile != null)
            {
                if (banner.ImageFile.ContentType != "image/jpeg" && banner.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                    return View();
                }

                if (banner.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only maximum 5Mb !");
                    return View();
                }

                banner.Image = FileManager.Save(_env.WebRootPath, "uploads/banners", banner.ImageFile);
            }

            _context.Banners.Add(banner);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/banners", banner.Image);
            }


            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Banner banner = _context.Banners.FirstOrDefault(x => x.Id == id);

            if (banner == null) return View("NotFoundPage");

            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Banner bnr)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Banners.Any(x => x.Order == bnr.Order && x.Id != bnr.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Banner banner = _context.Banners.FirstOrDefault(x => x.Id == bnr.Id);

            if (banner == null) return View("NotFoundPage");

            banner.Title = bnr.Title;
            banner.Order = bnr.Order;
            banner.Price = bnr.Price;
            banner.IsMain = bnr.IsMain;

            if (bnr.ImageFile != null)
            {
                if (bnr.ImageFile.ContentType != "image/jpeg" && bnr.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (bnr.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/banners", bnr.ImageFile);

                if (!string.IsNullOrWhiteSpace(banner.Image))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/banners", banner.Image);
                }

                banner.Image = newFileName;
            }
            else if (string.IsNullOrWhiteSpace(bnr.Image) && !string.IsNullOrWhiteSpace(banner.Image))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/banners", banner.Image);

                banner.Image = null;
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Banner banner = _context.Banners.FirstOrDefault(x => x.Id == id);

            if (banner == null) return View("NotFoundPage");

            return View(banner);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Banner bnr)
        {
            if (!ModelState.IsValid) return View();

            Banner banner = _context.Banners.FirstOrDefault(x => x.Id == bnr.Id);

            if (banner == null) return View("NotFoundPage");

            if (!string.IsNullOrWhiteSpace(banner.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/banners", banner.Image);
            }

            _context.Banners.Remove(banner);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
