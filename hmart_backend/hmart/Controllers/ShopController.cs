using hmart.DAL;
using hmart.Models;
using hmart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ShopController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int? categoryId, int? brandId, string searchName, int? tagId, int? colorId)
        {
            var query = _context.Products.AsQueryable();

            ViewBag.AllProductsCount = query.Count();

            if (categoryId != null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            if (brandId != null)
            {
                query = query.Where(x => x.BrandId == brandId);
            }

            if (searchName != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchName.ToLower()));
            }

            if (tagId != null)
            {
                query = query.Where(x => x.ProductTagProducts.Any(x => x.ProductTagId == tagId));
            }

            if (colorId != null)
            {
                query = query.Where(x => x.ProductColors.Any(x => x.ColorId == colorId));
            }

            int totalProducts = query.Count();

            ShopVM shopVM = new ShopVM
            {
                Setting = _context.Settings.FirstOrDefault(),

                SelectedPage = 1,
                TotalPages = (int)Math.Ceiling(totalProducts / 12d),
                TotalProducts = totalProducts,
                CategoryId = categoryId,
                BrandId = brandId,
                ColorId = colorId,
                TagId = tagId,
                SearchName = searchName,
                MinPrice = (int)Math.Round((double)(_context.Products.Min(p => p.DiscountPercent == null ? p.Price : (p.Price - p.Price * p.DiscountPercent / 100)))),
                MaxPrice = (int)Math.Round((double)(_context.Products.Max(p => p.DiscountPercent == null ? p.Price : (p.Price - p.Price * p.DiscountPercent / 100)))),

                Categories = _context.Categories
                .Include(c => c.Products).ToList(),

                Brands = _context.Brands
                .Include(c => c.Products).ToList(),

                Colors = _context.Colors
                .Include(c => c.ProductColors).ThenInclude(pc => pc.Product)
                .ToList(),

                Products = query
                .Include(c => c.Category)
                .Include(ptp => ptp.ProductTagProducts).ThenInclude(pt => pt.ProductTag)
                .Include(pi => pi.ProImages)
                .Take(12).ToList()
            };

            ////ViewBag.SelectedPage = 1;
            ////ViewBag.TotalPages = (int)Math.Ceiling(totalProducts / 12d);
            ////ViewBag.TotalProducts = totalProducts;
            ////ViewBag.CategoryId = categoryId;
            ////ViewBag.Name = name;
            ////ViewBag.TagId = tagId;
            ////List<Product> products = query
            ////    .Include(c => c.Category)
            ////    .Include(ptp => ptp.ProductTagProducts).ThenInclude(pt => pt.ProductTag)
            ////    .Take(12).ToList();

            return View(shopVM);
        }
        public IActionResult Detail(int id)
        {
            Product product = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.ProImages)
                .Include(x => x.Reviews).ThenInclude(x => x.AppUser)
                .Include(x => x.ProductTagProducts).ThenInclude(x => x.ProductTag)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .FirstOrDefault(x => x.Id == id);

            if (product == null) return View("NotFound");

            ProductDetailVM productDetailVM = new ProductDetailVM
            {
                Setting = _context.Settings.FirstOrDefault(),
                Product = product,
                RelatedProducts = _context.Products
                .Include(x=>x.Category)
                .Include(x=>x.ProImages)
                .Where(x => x.CategoryId == product.CategoryId 
                || x.BrandId == product.BrandId)
                .Take(8).ToList()
            };

            return View(productDetailVM);
        }

        public IActionResult GetDetail(int id)
        {
            Product product = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.Reviews)
                .Include(x => x.Category)
                .Include(x => x.ProductTagProducts).ThenInclude(x=>x.ProductTag)
                .FirstOrDefault(x=>x.Id == id);


            return PartialView("_DetailPartial",product);
        }

        public IActionResult AddToBasket(int id, int? count)
        {
            Product product = _context.Products.Include(x => x.BasketItems).FirstOrDefault(x => x.Id == id);

            BasketVM basketData = new BasketVM()
            {
                BasketItemVMs = new List<BasketItemVM>(),
                TotalPrice = 0,
                IsAddBtn=true
            };

            if (product.Count <= 0)
            {
                return NoContent();
            }

            if (count != null && product.Count < count)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created);
            }

            if (User.Identity.IsAuthenticated && _userManager.Users.Any(x => x.UserName == User.Identity.Name && x.IsAdmin == false))
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                BasketItem basketItem = product.BasketItems.FirstOrDefault(x => x.AppUserId == user.Id);
               
                if (basketItem != null)
                {
                    basketItem.Count += (count == null ? 1 : (int)count);
                }
                else
                {
                    basketItem = new BasketItem
                    {
                        AppUserId = user.Id,
                        Count = (count == null ? 1 : (int)count),
                        ProductId = id
                    };

                    product.BasketItems.Add(basketItem);
                }
               
                _context.SaveChanges();

                foreach (var item in _context.BasketItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProImages).Where(x => x.AppUser.UserName == User.Identity.Name).ToList())
                {
                    BasketItemVM basketItemVM = new BasketItemVM()
                    {
                        Product = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId),
                        Count = item.Count
                    };

                    if (basketItemVM.Product != null)
                    {
                        if (basketItemVM.Product.DiscountPercent == null)
                        {
                            basketData.TotalPrice += basketItemVM.Product.Price * basketItemVM.Count;
                        }
                        else
                        {
                            basketData.TotalPrice += (double)((basketItemVM.Product.Price - basketItemVM.Product.Price * basketItemVM.Product.DiscountPercent/100) * basketItemVM.Count);
                        }
                        basketData.BasketItemVMs.Add(basketItemVM);
                        basketData.Count++;
                    }
                }
            }
            else
            {
                var basket = HttpContext.Request.Cookies["Basket"];

                List<BasketCookieItemVM> basketCookieItemVMs = new List<BasketCookieItemVM>();

                if (basket == null)
                {
                    basketCookieItemVMs = new List<BasketCookieItemVM>()
                    {
                        new BasketCookieItemVM()
                        {
                            ProductId = product.Id,
                            Count = (count == null ? 1 : (int)count)
                         }
                     };
                }
                else
                {
                    basketCookieItemVMs = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);
                    BasketCookieItemVM basketCookieItemVM = basketCookieItemVMs.FirstOrDefault(x => x.ProductId == product.Id);
                    if (basketCookieItemVM == null)
                    {
                        basketCookieItemVM = new BasketCookieItemVM()
                        {
                            ProductId = product.Id,
                            Count = (count == null ? 1 : (int)count)
                        };
                        basketCookieItemVMs.Add(basketCookieItemVM);
                    }
                    else
                    {
                        basketCookieItemVM.Count+= (count == null ? 1 : (int)count);
                    }
                }

                var basketCookieItemsStr = JsonConvert.SerializeObject(basketCookieItemVMs);

                HttpContext.Response.Cookies.Append("Basket", basketCookieItemsStr);

                foreach (var item in basketCookieItemVMs)
                {

                    BasketItemVM basketItem = new BasketItemVM()
                    {
                        Product = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId),
                        Count = item.Count
                    };

                    if (basketItem.Product != null)
                    {
                        if (basketItem.Product.DiscountPercent == null)
                        {
                            basketData.TotalPrice += basketItem.Product.Price * basketItem.Count;
                        }
                        else
                        {
                            basketData.TotalPrice += (double)((basketItem.Product.Price - basketItem.Product.Price * basketItem.Product.DiscountPercent / 100) * basketItem.Count);
                        }
                        basketData.BasketItemVMs.Add(basketItem);
                        basketData.Count++;
                    }
                }
            }

            return PartialView("_BasketPartial", basketData);
        }

        public IActionResult DeleteFromBasket(int id, string isCanvas, string keyWord)
        {
            Product product = _context.Products.Include(x => x.BasketItems).FirstOrDefault(b => b.Id == id);

            BasketVM basketData = new BasketVM()
            {
                BasketItemVMs = new List<BasketItemVM>(),
                TotalPrice = 0,
                IsAddBtn = (isCanvas=="no")
            };

            if (User.Identity.IsAuthenticated && _userManager.Users.Any(x => x.UserName == User.Identity.Name && x.IsAdmin == false))
            {
                AppUser user = _context.AppUsers
               .Include(x => x.BasketItems)
               .FirstOrDefault(x => x.UserName.ToLower() == User.Identity.Name.ToLower());

                if (keyWord!="clear")
                {
                    BasketItem basketItem = product.BasketItems.FirstOrDefault(x => x.AppUserId == user.Id);
                    if (basketItem.Count > 1 && keyWord != "out")
                    {
                        basketItem.Count--;
                    }
                    else
                    {
                        product.BasketItems.RemoveAll(x => x.AppUserId == user.Id);
                    }
                }
                else
                {
                    _context.BasketItems.RemoveRange(user.BasketItems);
                }

                _context.SaveChanges();

                foreach (var item in _context.BasketItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProImages).Where(x => x.AppUser.UserName == User.Identity.Name).ToList())
                {
                    BasketItemVM basketItemVM = new BasketItemVM()
                    {
                        Product = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId),
                        Count = item.Count
                    };

                    if (basketItemVM.Product != null)
                    {
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
            }
            else
            {
                var basket = HttpContext.Request.Cookies["Basket"];
                List<BasketCookieItemVM> basketCookieItemVMs;


                basketCookieItemVMs = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);

                BasketCookieItemVM basketItemVM = basketCookieItemVMs.FirstOrDefault(x => x.ProductId == id);

                if (basketItemVM.Count > 1)
                {
                    basketCookieItemVMs.FirstOrDefault(x => x.ProductId == id).Count--;
                }
                else
                {
                    basketCookieItemVMs.RemoveAll(x => x.ProductId == id);
                }

                var basketCookieItemsStr = JsonConvert.SerializeObject(basketCookieItemVMs);
                HttpContext.Response.Cookies.Append("Basket", basketCookieItemsStr);



                foreach (var item in basketCookieItemVMs)
                {
                    BasketItemVM basketItemVMForCookie = new BasketItemVM()
                    {
                        Product = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId),
                        Count = item.Count
                    };

                    if (basketItemVMForCookie.Product != null)
                    {
                        if (basketItemVMForCookie.Product.DiscountPercent == null)
                        {
                            basketData.TotalPrice += basketItemVMForCookie.Product.Price * basketItemVMForCookie.Count;
                        }
                        else
                        {
                            basketData.TotalPrice += (double)((basketItemVMForCookie.Product.Price - basketItemVMForCookie.Product.Price * basketItemVMForCookie.Product.DiscountPercent / 100) * basketItemVMForCookie.Count);
                        }
                        basketData.BasketItemVMs.Add(basketItemVMForCookie);
                        basketData.Count++;
                    }

                }
            }

            return PartialView("_BasketPartial", basketData);
        }

        [Authorize(Roles = "Member")]
        public IActionResult CartList(string keyWord)
        {
            if (keyWord != null)    
            {
                ViewData["keyWord"] = keyWord;
            }
            AppUser user = _context.AppUsers
                .Include(x => x.BasketItems)
                .FirstOrDefault(x => x.UserName.ToLower() == User.Identity.Name.ToLower());

            if (user == null) return View("NotFound");

            CartVM cartVM = new CartVM
            {
                BasketItemVMs = new List<BasketItemVM>(),
                TotalPrice = 0
            };

            foreach (var item in user.BasketItems)
            {
                Product basketProduct = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId);

                BasketItemVM basketItemVM = new BasketItemVM
                {
                    Product = basketProduct,
                    Count = item.Count
                };

                if (basketProduct.DiscountPercent != null)
                {
                    cartVM.TotalPrice += (double)(basketProduct.Price - basketProduct.Price * basketProduct.DiscountPercent / 100) * item.Count;
                }
                else
                {
                    cartVM.TotalPrice += basketProduct.Price * item.Count;
                }

                cartVM.BasketItemVMs.Add(basketItemVM);
                cartVM.Count++;
            }

            return PartialView("_CartListPartial", cartVM);
        }

        public IActionResult AddToWishList(int id)
        {
            Product product = _context.Products.Include(x => x.WishListItems).FirstOrDefault(x => x.Id == id);

            WishListVM wishListVM = new WishListVM
            {
                Products = new List<Product>(),
                IsAddBtn = true
            };

            if (product.Count == 0)
            {
                return NoContent();
            }

            if (User.Identity.IsAuthenticated && _userManager.Users.Any(x => x.UserName == User.Identity.Name && x.IsAdmin == false))
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;


                WishListItem wishListItem = product.WishListItems?.FirstOrDefault(x => x.AppUserId == user.Id);
                if (wishListItem != null)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created);
                }
                else
                {
                    wishListItem = new WishListItem
                    {
                        AppUserId = user.Id,
                        ProductId = id
                    };

                    product.WishListItems.Add(wishListItem);
                }

                _context.SaveChanges();

                foreach (var item in _context.WishListItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProImages).Where(x => x.AppUser.UserName == User.Identity.Name).ToList())
                {
                    Product wishProduct = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId);

                    if (wishProduct != null)
                    {
                        wishListVM.Products.Add(wishProduct);
                        wishListVM.Count++;
                    }
                }
            }
            else
            {
                var basket = HttpContext.Request.Cookies["WishList"];

                List<WishListCookieItemVM> wishListCookieItemVMs = new List<WishListCookieItemVM>();

                if (basket == null)
                {
                    wishListCookieItemVMs = new List<WishListCookieItemVM>()
                    {
                        new WishListCookieItemVM()
                        {
                            ProductId = product.Id
                         }
                     };
                }
                else
                {
                    wishListCookieItemVMs = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(basket);
                    WishListCookieItemVM wishListCookieItemVM = wishListCookieItemVMs.FirstOrDefault(x => x.ProductId == product.Id);
                    if (wishListCookieItemVM == null)
                    {
                        wishListCookieItemVM = new WishListCookieItemVM()
                        {
                            ProductId = product.Id
                        };
                        wishListCookieItemVMs.Add(wishListCookieItemVM);
                    }
                    else
                    {
                        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created);
                    }
                }

                var wishListCookieItemVMsStr = JsonConvert.SerializeObject(wishListCookieItemVMs);

                HttpContext.Response.Cookies.Append("WishList", wishListCookieItemVMsStr);

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

            return PartialView("_WishListPartial", wishListVM);
        }

        public IActionResult DeleteFromWishList (int id, string isCanvas)
        {
            Product product = _context.Products.Include(x => x.WishListItems).FirstOrDefault(b => b.Id == id);

            WishListVM wishListVM = new WishListVM()
            {
                Products = new List<Product>(),
                IsAddBtn = (isCanvas==null?false:true)
            };

            if (User.Identity.IsAuthenticated && _userManager.Users.Any(x => x.UserName == User.Identity.Name && x.IsAdmin == false))
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                WishListItem wishListItem = product.WishListItems.FirstOrDefault(x => x.AppUserId == user.Id);
                
                    product.WishListItems.Remove(wishListItem);

                _context.SaveChanges();

                foreach (var item in _context.WishListItems.Include(x => x.AppUser).Include(x => x.Product).ThenInclude(x => x.ProImages).Where(x => x.AppUser.UserName == User.Identity.Name).ToList())
                {
                    Product wishProduct = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == item.ProductId);

                    if (wishProduct != null)
                    {
                        wishListVM.Products.Add(wishProduct);
                        wishListVM.Count++;
                    }
                }
            }
            else
            {
                var wishList = HttpContext.Request.Cookies["WishList"];
                List<WishListCookieItemVM> wishListCookieItemVMs;


                wishListCookieItemVMs = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishList);

                WishListCookieItemVM wishListCookieItemVM = wishListCookieItemVMs.FirstOrDefault(x => x.ProductId == id);

                    wishListCookieItemVMs.Remove(wishListCookieItemVM);

                var wishListCookieItemVMsStr = JsonConvert.SerializeObject(wishListCookieItemVMs);
                HttpContext.Response.Cookies.Append("WishList", wishListCookieItemVMsStr);


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

            return PartialView("_WishListPartial", wishListVM);
        }

        public IActionResult Search(string searchName)
        {
            if (searchName == null) return Json(new { status = 404 });

            List<Product> products = _context.Products.Include(x=>x.ProImages).Where(x => x.Name.ToLower().Contains(searchName.ToLower())).ToList();

            if (products == null) return Json(new { status = 404 });

            return PartialView("_ProductsSearchPartial", products);
        }

        public IActionResult Pagenation(int? categoryId, int? brandId, string searchName, int? tagId, int? colorId, string sortingBy,int? min, int? max, int page = 1)
        {
            var query = _context.Products.AsQueryable();
            if (categoryId != null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            if (brandId != null)
            {
                query = query.Where(x => x.BrandId == brandId);
            }

            if (searchName != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchName.ToLower()));
            }

            if (tagId != null)
            {
                query = query.Where(x => x.ProductTagProducts.Any(x => x.ProductTagId == tagId));
            }

            if (colorId != null)
            {
                query = query.Where(x => x.ProductColors.Any(x => x.ColorId == colorId));
            }

            if (sortingBy != null)
            {
                switch (sortingBy)
                {
                    case "a_to_z":
                        query = query.OrderBy(x => x.Name);
                            break;
                    case "z_to_a":
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case "low_to_high":
                        query = query.OrderBy(x => x.DiscountPercent==null?x.Price:x.Price-x.Price*x.DiscountPercent/100);
                        break;
                    case "high_to_low":
                        query = query.OrderByDescending(x => x.DiscountPercent == null ? x.Price : x.Price - x.Price * x.DiscountPercent / 100);
                        break;
                    case "new":
                        query = query.OrderByDescending(x => x.CreatedAt);
                        break;
                    case "old":
                        query = query.OrderBy(x => x.CreatedAt);
                        break;
                    default:
                        sortingBy = null;
                        break;
                }
            }

            if (min != null && max != null )
            {
                double minPrice = (double)min;
                double maxPrice = (double)max;

                query = query.Where(x => (x.DiscountPercent == null ? x.Price : (x.Price - x.Price * x.DiscountPercent / 100)) > minPrice &&
                (x.DiscountPercent == null ? x.Price : (x.Price - x.Price * x.DiscountPercent / 100)) < maxPrice);

                }

            int totalProducts = query.Count();

            PagenationVM pagenationVM = new PagenationVM
            {
                SelectedPage = page,
                TotalPages = (int)Math.Ceiling(totalProducts / 12d),
                TotalProducts = totalProducts,
                CategoryId = categoryId,
                BrandId = brandId,
                ColorId = colorId,
                TagId = tagId,
                SearchName = searchName,
                SortingBy = sortingBy,
                Products = query
                .Include(c => c.Category)
                .Include(ptp => ptp.ProductTagProducts).ThenInclude(pt => pt.ProductTag)
                .Include(pi => pi.ProImages)
                .Skip((page-1)*12).Take(12).ToList()
            };

            return PartialView("_ProductsPartial", pagenationVM);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(AddReviewVM addReviewVM)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "You can't submit without rate or your message length can't be longer from 250 !";
                return RedirectToAction("detail", "shop", new { id = addReviewVM.ProductId });
            }
               
            Product product = _context.Products.Include(x => x.Reviews).FirstOrDefault(x => x.Id == addReviewVM.ProductId);
            if (product == null)
                return View("NotFound");

            if (_context.Reviews.Any(x => x.ProductId == addReviewVM.ProductId && x.IsAccepting == true && x.AppUserId == user.Id))
            {
                TempData["Error"] = "You can't again give rate to this product!";
                return RedirectToAction("detail", "shop", new { id = addReviewVM.ProductId });
            }


            Review review = new Review
            {
                ProductId = addReviewVM.ProductId,
                AppUserId = user.Id,
                Message = addReviewVM.Message,
                Rate = addReviewVM.Rate,
                CreatedAt = DateTime.UtcNow
            };
            product.Reviews.Add(review);

            _context.SaveChanges();

            TempData["Succeed"] = "Your review sending.";

            return RedirectToAction("detail", "shop", new { id = addReviewVM.ProductId });
        }
    }
}
