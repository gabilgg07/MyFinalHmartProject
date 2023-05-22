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
    public class ProductTagController : Controller
    {
        private readonly AppDbContext _context;

        public ProductTagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductTag> productTags = _context.ProductTags.ToList();

            return View(productTags);
        }

        public IActionResult Create()
        {
            ViewBag.ProductTagsCount = _context.ProductTags.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductTag productTag)
        {
            ViewBag.ProductTagsCount = _context.ProductTags.Count();
            if (!ModelState.IsValid) return View();

            if (_context.ProductTags.Any(x => x.Order == productTag.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            ProductTag newProductTag = new ProductTag
            {
                Name = productTag.Name,
                Order = productTag.Order
            };

            _context.ProductTags.Add(newProductTag);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ProductTagsCount = _context.ProductTags.Count();

            ProductTag productTag = _context.ProductTags.FirstOrDefault(x => x.Id == id);

            if (productTag == null) return View("NotFoundPage");

            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductTag proTag)
        {

            ViewBag.ProductTagsCount = _context.ProductTags.Count();
            if (!ModelState.IsValid) return View();

            if (_context.ProductTags.Any(x => x.Order == proTag.Order && x.Id != proTag.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            ProductTag productTag = _context.ProductTags.FirstOrDefault(x => x.Id == proTag.Id);

            if (productTag == null) return View("NotFoundPage");

            productTag.Name = proTag.Name;
            productTag.Order = proTag.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            ProductTag productTag = _context.ProductTags.FirstOrDefault(x => x.Id == id);

            if (productTag == null) return View("NotFoundPage");

            return View(productTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProductTag proTag)
        {
            ProductTag productTag = _context.ProductTags.FirstOrDefault(x => x.Id == proTag.Id);

            if (productTag == null) return View("NotFoundPage");

            _context.ProductTags.Remove(productTag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
