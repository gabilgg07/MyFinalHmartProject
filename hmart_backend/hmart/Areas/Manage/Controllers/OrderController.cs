using hmart.DAL;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<Order> orders = _context.Orders.Include(x => x.OrderItems)
                .Include(x => x.AppUser)
                .ToList();

            return View(orders);
        }

        public IActionResult Detail(int id)
        {
            Order order = _context.Orders.Include(x => x.AppUser).Include(x => x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (order == null) return View("NotFoundPage");

            return View(order);
        }

        public IActionResult Accept(int id, string note)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return Json(new { status = 404 });

            order.Status = true;
            order.AdminNote = note;

            _context.SaveChanges();

            return Json(new { status = 200 });
        }

        public IActionResult Reject(int id, string note)
        {
            Order order = _context.Orders
                .Include(x=>x.OrderItems).ThenInclude(x=>x.Product)
                .FirstOrDefault(x => x.Id == id);

            if (order == null) return Json(new { status = 404 });

            if (note == null)
            {
                return Json(new { status = 400 });
            }

            order.Status = false;
            order.AdminNote = note;

            foreach (var item in order.OrderItems)
            {
                _context.Products.FirstOrDefault(x => x.Id == item.ProductId).Count += item.Count;
            }

            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
