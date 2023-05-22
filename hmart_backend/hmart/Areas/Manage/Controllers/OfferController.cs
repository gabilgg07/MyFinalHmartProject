using hmart.DAL;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,EditorAdmin")]
    public class OfferController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public OfferController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Offer> offers = _context.Offers.Include(x => x.Product).ToList();

            return View(offers);
        }

        public IActionResult Create()
        {
            ViewBag.OffersCount = _context.Offers.Count();
            ViewBag.IsOnOfferProducts = _context.Products.Where(x => x.IsOnOffer).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Offer offer)
        {
            ViewBag.OffersCount = _context.Offers.Count();
            ViewBag.IsOnOfferProducts = _context.Products.Where(x => x.IsOnOffer).ToList();

            if (!ModelState.IsValid) return View();

            if (_context.Offers.Any(x => x.Order == offer.Order))
            {
                ModelState.AddModelError("Order", "Order is required!");
                return View();
            }

            _context.Offers.Add(offer);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.OffersCount = _context.Offers.Count();
            ViewBag.IsOnOfferProducts = _context.Products.Where(x => x.IsOnOffer).ToList();
            Offer offer = _context.Offers.FirstOrDefault(x => x.Id == id);

            if (offer == null) return View("NotFoundPage");

            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Offer offer)
        {
            ViewBag.OffersCount = _context.Offers.Count();
            ViewBag.IsOnOfferProducts = _context.Products.Where(x => x.IsOnOffer).ToList();

            if (!ModelState.IsValid) return View();

            Offer existOffer = _context.Offers.FirstOrDefault(x => x.Id == offer.Id);

            if (existOffer == null) return View("NotFoundPage");

            existOffer.ProductId = offer.ProductId;
            existOffer.IsMain = offer.IsMain;
            existOffer.Order = offer.Order;
            existOffer.OfferTime = offer.OfferTime;
            existOffer.Predecessor = offer.Predecessor;
            existOffer.SupporType = offer.SupporType;
            existOffer.Cushioning = offer.Cushioning;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            ViewBag.IsOnOfferProducts = _context.Products.Where(x => x.IsOnOffer).ToList();

            Offer offer = _context.Offers.FirstOrDefault(x => x.Id == id);

            if (offer == null) return View("NotFoundPage");

            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Offer offer)
        {
            ViewBag.IsOnOfferProducts = _context.Products.Where(x => x.IsOnOffer).ToList();

            Offer existOffer = _context.Offers.FirstOrDefault(x => x.Id == offer.Id);

            if (existOffer == null) return View("NotFoundPage");

            _context.Offers.Remove(existOffer);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
