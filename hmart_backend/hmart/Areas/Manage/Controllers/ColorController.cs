using hmart.DAL;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{

    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,EditorAdmin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Color> colors = _context.Colors.ToList();

            return View(colors);
        }

        public IActionResult Create()
        {
            ViewBag.ColorsCount = _context.Colors.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Color color)
        {

            ViewBag.ColorsCount = _context.Colors.Count();

            if (!ModelState.IsValid) return View();

            if (_context.Colors.Any(x => x.Order == color.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Regex rgx = new Regex(@"^(?:[0-9a-fA-F]{3,4}){1,2}$");

            if (!rgx.IsMatch(color.Code))
            {
                ModelState.AddModelError("Code", "Code is not a valid hex code!");
                return View();
            }

            if (_context.Colors.Any(x => x.Code == color.Code))
            {
                ModelState.AddModelError("Code", "Code is required!");
                return View();
            }

            Color newColor = new Color
            {
                Name = color.Name,
                Code = color.Code,
                Order = color.Order
            };

            _context.Colors.Add(newColor);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ColorsCount = _context.Colors.Count();

            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);

            if (color == null) return View("NotFoundPage");

            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Color clr)
        {
            ViewBag.ColorsCount = _context.Colors.Count();

            if (!ModelState.IsValid) return View();

            if (_context.Colors.Any(x => x.Order == clr.Order && x.Id != clr.Id))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            Regex rgx = new Regex(@"^(?:[0-9a-fA-F]{3,4}){1,2}$");

            if (!rgx.IsMatch(clr.Code))
            {
                ModelState.AddModelError("Code", "Code is not a valid hex code!");
                return View();
            }

            if (_context.Colors.Any(x => x.Code == clr.Code && x.Id != clr.Id))
            {
                ModelState.AddModelError("Code", "Code is required!");
                return View(clr);
            }

            Color color = _context.Colors.FirstOrDefault(x => x.Id == clr.Id);

            if (color == null) return View("NotFoundPage");

            color.Name = clr.Name;
            color.Code = clr.Code;
            color.Order = clr.Order;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == id);

            if (color == null) return View("NotFoundPage");

            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Color clr)
        {
            Color color = _context.Colors.FirstOrDefault(x => x.Id == clr.Id);

            if (color == null) return View("NotFoundPage");

            _context.Colors.Remove(color);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
