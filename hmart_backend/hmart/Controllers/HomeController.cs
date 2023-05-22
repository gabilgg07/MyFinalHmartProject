using hmart.DAL;
using hmart.Models;
using hmart.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Setting = _context.Settings.FirstOrDefault(),

                Sliders = _context.Sliders
                .OrderBy(x => x.Order)
                .Take(4).ToList(),

                MainBanner = _context.Banners.FirstOrDefault(x => x.IsMain),

                Banners = _context.Banners
                .Where(x => x.IsMain == false)
                .OrderBy(x => x.Order)
                .Take(2).ToList(),

                NewProducts = _context.Products
                .Include(x=>x.ProImages)
                .Include(x=>x.Category)
                .OrderByDescending(x => x.CreatedAt)
                .Take(8).ToList(),

                TopRatedProducts = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Rate)
                .Take(8).ToList(),

                FeaturedProducts = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.Category)
                .Where(x => x.IsOnOffer)
                .Take(8).ToList(),

                MainOffer = _context.Offers
                .Include(x => x.Product).ThenInclude(x=>x.ProImages)
                .FirstOrDefault(x => x.IsMain),

                Offers = _context.Offers
                .Where(x=>x.IsMain==false)
                .Include(x=>x.Product)
                .OrderBy(x=>x.Order)
                .Take(2).ToList(),

                Testimonials = _context.Testimonials
                .OrderBy(x => x.Order)
                .Take(5).ToList(),

                Partners = _context.Partners
                .OrderBy(x => x.Order)
                .Take(8).ToList(),

                Blogs = _context.Blogs
                .OrderByDescending(x => x.Date)
                .Take(2).ToList()
            };

            return View(homeVM);
        }

        public IActionResult About()
        {
            AboutVM aboutVM = new AboutVM
            {
                Setting = _context.Settings.FirstOrDefault(),

                Teams = _context.Teams
                .OrderBy(x => x.Order)
                .Take(3).ToList(),

                Features = _context.Features
                .OrderBy(x => x.Order)
                .Take(3).ToList(),

                Testimonials = _context.Testimonials
                .OrderBy(x=>x.Order)
                .Take(5).ToList(),

                Blogs = _context.Blogs
                .OrderByDescending(x => x.Date)
                .Take(2).ToList()
            };

            return View(aboutVM);
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
