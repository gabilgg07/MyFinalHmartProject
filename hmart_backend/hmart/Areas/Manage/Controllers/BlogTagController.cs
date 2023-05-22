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
    public class BlogTagController : Controller
    {
        private readonly AppDbContext _context;

        public BlogTagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<BlogTag> blogTags = _context.BlogTags.ToList();

            return View(blogTags);
        }

        public IActionResult Create()
        {
            ViewBag.BlogTagsCount = _context.BlogTags.Count();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogTag blogTag)
        {
            ViewBag.BlogTagsCount = _context.BlogTags.Count();

            if (!ModelState.IsValid) return View();

            if (_context.BlogTags.Any(x => x.Order == blogTag.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            BlogTag newBlogTag = new BlogTag
            {
                Name = blogTag.Name,
                Order = blogTag.Order
            };

            _context.BlogTags.Add(newBlogTag);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.BlogTagsCount = _context.BlogTags.Count();

            BlogTag blogTag = _context.BlogTags.FirstOrDefault(x => x.Id == id);

            if (blogTag == null) return View("NotFoundPage");

            return View(blogTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogTag bgTag)
        {

            ViewBag.BlogTagsCount = _context.BlogTags.Count();

            if (!ModelState.IsValid) return View();

            if (_context.BlogTags.Any(x => x.Order == bgTag.Order && x.Id != bgTag.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            BlogTag blogTag = _context.BlogTags.FirstOrDefault(x => x.Id == bgTag.Id);

            if (blogTag == null) return View("NotFoundPage");

            blogTag.Name = bgTag.Name;
            blogTag.Order = bgTag.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            BlogTag blogTag = _context.BlogTags.FirstOrDefault(x => x.Id == id);

            if (blogTag == null) return View("NotFoundPage");

            return View(blogTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BlogTag bgTag)
        {
            BlogTag blogTag = _context.BlogTags.FirstOrDefault(x => x.Id == bgTag.Id);

            if (blogTag == null) return View("NotFoundPage");

            _context.BlogTags.Remove(blogTag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
