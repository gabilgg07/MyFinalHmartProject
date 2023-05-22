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
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TestimonialController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {

            List<Testimonial> testimonials = _context.Testimonials.ToList();

            return View(testimonials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Testimonial testimonial)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Testimonials.Any(x => x.Order == testimonial.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            if (testimonial.ImageFile != null)
            {
                if (testimonial.ImageFile.ContentType != "image/jpeg" && testimonial.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                    return View();
                }

                if (testimonial.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "You can choose file only maximum 5Mb !");
                    return View();
                }

                testimonial.Image = FileManager.Save(_env.WebRootPath, "uploads/testimonials", testimonial.ImageFile);
            }

            _context.Testimonials.Add(testimonial);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/testimonials", testimonial.Image);
            }


            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);

            if (testimonial == null) return View("NotFoundPage");

            return View(testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Testimonial tsm)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Testimonials.Any(x => x.Order == tsm.Order && x.Id != tsm.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == tsm.Id);

            if (testimonial == null) return View("NotFoundPage");

            testimonial.FullName = tsm.FullName;
            testimonial.Position = tsm.Position;
            testimonial.Desc = tsm.Desc;
            testimonial.Order = tsm.Order;

            if (tsm.ImageFile != null)
            {
                if (tsm.ImageFile.ContentType != "image/jpeg" && tsm.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (tsm.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/testimonials", tsm.ImageFile);

                if (!string.IsNullOrWhiteSpace(testimonial.Image))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/testimonials", testimonial.Image);
                }

                testimonial.Image = newFileName;
            }
            else if (string.IsNullOrWhiteSpace(tsm.Image) && !string.IsNullOrWhiteSpace(testimonial.Image))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/testimonials", testimonial.Image);

                testimonial.Image = null;
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);

            if (testimonial == null) return View("NotFoundPage");

            return View(testimonial);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Testimonial tsm)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == tsm.Id);

            if (testimonial == null) return View("NotFoundPage");

            if (!string.IsNullOrWhiteSpace(testimonial.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/testimonials", testimonial.Image);
            }

            _context.Testimonials.Remove(testimonial);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
