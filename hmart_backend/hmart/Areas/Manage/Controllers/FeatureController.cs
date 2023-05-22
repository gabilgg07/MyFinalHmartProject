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
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FeatureController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Feature> features = _context.Features.ToList();

            return View(features);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Features.Any(x => x.Order == feature.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            if (feature.ImageFile != null)
            {
                if (feature.ImageFile.ContentType != "image/jpeg" && feature.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                    return View();
                }

                if (feature.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only maximum 5Mb !");
                    return View();
                }

                feature.Image = FileManager.Save(_env.WebRootPath, "uploads/features", feature.ImageFile);
            }

            _context.Features.Add(feature);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/features", feature.Image);
            }

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);

            if (feature == null) return View("NotFoundPage");

            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Feature ftr)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Features.Any(x => x.Order == ftr.Order && x.Id != ftr.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Feature feature = _context.Features.FirstOrDefault(x => x.Id == ftr.Id);

            if (feature == null) return View("NotFoundPage");

            feature.Title = ftr.Title;
            feature.Subtitle = ftr.Subtitle;
            feature.Order = ftr.Order;

            if (ftr.ImageFile != null)
            {
                if (ftr.ImageFile.ContentType != "image/jpeg" && ftr.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (ftr.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/features", ftr.ImageFile);

                if (!string.IsNullOrWhiteSpace(feature.Image))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/features", feature.Image);
                }

                feature.Image = newFileName;
            }
            else if (string.IsNullOrWhiteSpace(ftr.Image) && !string.IsNullOrWhiteSpace(feature.Image))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/features", feature.Image);

                feature.Image = null;
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);

            if (feature == null) return View("NotFoundPage");

            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Feature ftr)
        {
            if (!ModelState.IsValid) return View();

            Feature feature = _context.Features.FirstOrDefault(x => x.Id == ftr.Id);

            if (feature == null) return View("NotFoundPage");

            if (!string.IsNullOrWhiteSpace(feature.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/features", feature.Image);
            }

            _context.Features.Remove(feature);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
