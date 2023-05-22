using hmart.DAL;
using hmart.Helpers;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,EditorAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();

            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Sliders.Any(x => x.Order == slider.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                    return View();
                }

                if (slider.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only maximum 5Mb !");
                    return View();
                }

                slider.Image = FileManager.Save(_env.WebRootPath, "uploads/sliders", slider.ImageFile);
            }

            _context.Sliders.Add(slider);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);
            }


            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null) return View("NotFoundPage");

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider sld)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Sliders.Any(x => x.Order == sld.Order && x.Id != sld.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == sld.Id);

            if (slider == null) return View("NotFoundPage");

            slider.Title = sld.Title;
            slider.Subtitle = sld.Subtitle;
            slider.BtnText = sld.BtnText;
            slider.Order = sld.Order;

            if (sld.ImageFile != null)
            {
                if (sld.ImageFile.ContentType != "image/jpeg" && sld.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (sld.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/sliders", sld.ImageFile);

                if (!string.IsNullOrWhiteSpace(slider.Image))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);
                }

                slider.Image = newFileName;
            }
            else if (string.IsNullOrWhiteSpace(sld.Image) && !string.IsNullOrWhiteSpace(slider.Image))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);

                slider.Image = null;
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null) return View("NotFoundPage");

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Slider sld)
        {
            if (!ModelState.IsValid) return View();

            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == sld.Id);

            if (slider == null) return View("NotFoundPage");

            if (!string.IsNullOrWhiteSpace(slider.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);
            }

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
