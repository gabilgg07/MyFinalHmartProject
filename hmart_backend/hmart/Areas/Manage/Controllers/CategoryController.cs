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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();

            if (_context.Categories.Any(x => x.Order == category.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            _context.Categories.Add(category);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return View("NotFoundPage");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category ctg)
        {

            if (!ModelState.IsValid) return View();

            if (_context.Categories.Any(x => x.Order == ctg.Order && x.Id != ctg.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Category category = _context.Categories.FirstOrDefault(x => x.Id == ctg.Id);

            if (category == null) return View("NotFoundPage");

            category.Name = ctg.Name;
            category.Order = ctg.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return View("NotFoundPage");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category ctg)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == ctg.Id);

            if (category == null) return View("NotFoundPage");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
