using hmart.DAL;
using hmart.Models;
using hmart.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }

        public BasketVM GetBasket()
        {
            var basket = _httpContextAccessor.HttpContext.Request.Cookies["Basket"];

            BasketVM basketData = new BasketVM()
            {
                BasketItemVMs = new List<BasketItemVM>(),
                TotalPrice = 0
            };

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && _userManager.Users.Any(x => x.UserName == _httpContextAccessor.HttpContext.User.Identity.Name && x.IsAdmin == false))
            {
                var basketItems = _context.BasketItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProImages).Where(x => x.AppUser.UserName == _httpContextAccessor.HttpContext.User.Identity.Name);
                foreach (var item in basketItems)
                {
                    BasketItemVM basketItemVM = new BasketItemVM
                    {
                        Product = item.Product,
                        Count = item.Count
                    };

                    if (basketItemVM.Product.DiscountPercent == null)
                    {
                        basketData.TotalPrice += basketItemVM.Product.Price * basketItemVM.Count;
                    }
                    else
                    {
                        basketData.TotalPrice += (double)((basketItemVM.Product.Price - basketItemVM.Product.Price * basketItemVM.Product.DiscountPercent / 100) * basketItemVM.Count);
                    }

                    basketData.BasketItemVMs.Add(basketItemVM);
                    basketData.Count++;
                }
            }
            else
            {
                if (basket != null)
                {
                    List<BasketCookieItemVM> basketCookieItemVMs = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);



                    foreach (var item in basketCookieItemVMs)
                    {

                        BasketItemVM basketItemVMforCookie = new BasketItemVM()
                        {
                            Product = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId),
                            Count = item.Count
                        };

                        if (basketItemVMforCookie.Product != null)
                        {
                            if (basketItemVMforCookie.Product.DiscountPercent == null)
                            {
                                basketData.TotalPrice += basketItemVMforCookie.Product.Price * basketItemVMforCookie.Count;
                            }
                            else
                            {
                                basketData.TotalPrice += (double)((basketItemVMforCookie.Product.Price - basketItemVMforCookie.Product.Price * basketItemVMforCookie.Product.DiscountPercent / 100) * basketItemVMforCookie.Count);
                            }
                            basketData.BasketItemVMs.Add(basketItemVMforCookie);
                            basketData.Count++;
                        }
                    }
                }
            }

            return basketData;
        }

        public WishListVM GetWishList()
        {
            var wishList = _httpContextAccessor.HttpContext.Request.Cookies["WishList"];

            WishListVM wishListVM = new WishListVM
            {
                Products = new List<Product>()
            };

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && _userManager.Users.Any(x => x.UserName == _httpContextAccessor.HttpContext.User.Identity.Name && x.IsAdmin == false))
            {
                var wishListItems = _context.WishListItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProImages).Where(x => x.AppUser.UserName == _httpContextAccessor.HttpContext.User.Identity.Name);
                foreach (var item in wishListItems)
                {
                    Product wishProduct = item.Product;

                    wishListVM.Products.Add(wishProduct);
                    wishListVM.Count++;
                }
            }
            else
            {
                if (wishList != null)
                {
                    List<WishListCookieItemVM> wishListCookieItemVMs = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishList);



                    foreach (var item in wishListCookieItemVMs)
                    {
                         Product wishProductForCookie = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId);

                        if (wishProductForCookie != null)
                        {
                            wishListVM.Products.Add(wishProductForCookie);
                            wishListVM.Count++;
                        }
                    }
                }
            }

            return wishListVM;
        }
    }
}
