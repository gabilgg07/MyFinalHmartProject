using hmart.DAL;
using hmart.Models;
using hmart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Checkout(string isExpress, string shippingPrice)
        {
            if (TempData["isExpress"] != null)
            {
                isExpress = (string)TempData["isExpress"];
            }

            if (TempData["shippingPrice"] != null)  
            {
                shippingPrice = (string)TempData["shippingPrice"];
            }

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            OrderCreateVM orderCreateVM = new OrderCreateVM()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Phone = user.PhoneNumber,
                Country = user.Country,
                City = user.City,
                StateOrRegion = user.StateOrRegion,
                ZipOrPostalCode = user.ZipOrPostalCode,
                Address = user.Address,
                BasketItems = _context.BasketItems.Include(x => x.Product).Where(x => x.AppUserId == user.Id).ToList()
            };

            if (isExpress == "yes")
            {
                orderCreateVM.IsExpress = true;
            }
            else if (isExpress == "no")
            {
                orderCreateVM.IsExpress = false;
            }

            if (Double.TryParse(shippingPrice, out double shPrice))
            {
                if (shPrice > 0)
                {
                    orderCreateVM.ShippingPrice = shPrice;
                }
            }

            return View(orderCreateVM);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderCreateVM orderVM)
        {
            if (orderVM.IsExpress!=null)
            {
                TempData["isExpress"] = (orderVM.IsExpress==true?"yes":"no");
            }

            if (orderVM.ShippingPrice != null)
            {
                TempData["shippingPrice"] = orderVM.ShippingPrice.ToString() ;
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please, complete order!";
                return RedirectToAction("checkout", orderVM);
            }

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<BasketItem> basketItems = await _context.BasketItems.Include(x => x.Product).ThenInclude(x=>x.ProImages)
                .Where(x => x.AppUserId == user.Id).ToListAsync();

            if (basketItems.Count() == 0)
            {
                TempData["Error"] = "Basket is Empty!";
                return RedirectToAction("checkout");
            }

            Order order = new Order
            {
                Address = orderVM.Address,
                Country = orderVM.Country,
                City = orderVM.City,
                StateOrRegion = orderVM.StateOrRegion,
                ZipOrPostalCode = orderVM.ZipOrPostalCode,
                Phone = orderVM.Phone,
                Note = orderVM.Note,
                AppUserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            foreach (var basketItem in basketItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    ProductId = basketItem.ProductId,
                    Count = basketItem.Count,
                    Name = basketItem.Product.Name,
                    Image = basketItem.Product.GetPosterImage()
                };

                _context.Products.FirstOrDefault(x => x.Id == basketItem.ProductId).Count -= basketItem.Count; 

                orderItem.Price = basketItem.Product.DiscountPercent == null ?
                    basketItem.Product.Price : 
                    (double)(basketItem.Product.Price-basketItem.Product.Price*basketItem.Product.DiscountPercent/100);

                order.OrderItems.Add(orderItem);
                order.TotalPrice += orderItem.Price * orderItem.Count;
            }

            if (orderVM.IsExpress!=null)
            {
                order.IsExpress = orderVM.IsExpress;
            }

            if (orderVM.ShippingPrice>0)
            {
                order.ShippingPrice = (double)orderVM.ShippingPrice;
                order.TotalPrice += (double)orderVM.ShippingPrice;
            }

            await _context.Orders.AddAsync(order);

            _context.BasketItems.RemoveRange(basketItems);
            await _context.SaveChangesAsync();

            TempData["Succeed"] = "Order created successfuly!";

            return RedirectToAction("index", "home");
        }
    }
}
