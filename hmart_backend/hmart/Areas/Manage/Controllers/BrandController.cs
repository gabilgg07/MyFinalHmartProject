using hmart.DAL;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,EditorAdmin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Brand> brands = _context.Brands.ToList();

            return View(brands);
        }

        public IActionResult Create()
        {
            ViewBag.BrandsCount = _context.Brands.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            ViewBag.BrandsCount = _context.Brands.Count();

            if (!ModelState.IsValid) return View();

            if (_context.Brands.Any(x => x.Order == brand.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Brand newBrand = new Brand
            {
                Name = brand.Name,
                Order = brand.Order
            };

            _context.Brands.Add(newBrand);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.BrandsCount = _context.Brands.Count();

            Brand brand = _context.Brands.FirstOrDefault(x => x.Id == id);

            if (brand == null) return View("NotFoundPage");

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brnd)
        {

            ViewBag.BrandsCount = _context.Brands.Count();

            if (!ModelState.IsValid) return View();

            if (_context.Brands.Any(x => x.Order == brnd.Order && x.Id != brnd.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Brand brand = _context.Brands.FirstOrDefault(x => x.Id == brnd.Id);

            if (brand == null) return View("NotFoundPage");

            brand.Name = brnd.Name;
            brand.Order = brnd.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Brand brand = _context.Brands.FirstOrDefault(x => x.Id == id);

            if (brand == null) return View("NotFoundPage");

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Brand brnd)
        {
            Brand brand = _context.Brands.FirstOrDefault(x => x.Id == brnd.Id);

            if (brand == null) return View("NotFoundPage");

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
