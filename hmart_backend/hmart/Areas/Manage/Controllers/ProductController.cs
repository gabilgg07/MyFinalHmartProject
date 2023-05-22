using hmart.DAL;
using hmart.Helpers;
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
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products
                .Include(x=>x.ProImages)
                .Include(x=>x.Reviews)
                .ToList();

            return View(products);
        }

        public IActionResult Detail(int id)
        {
            Product product = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.ProductTagProducts).ThenInclude(x => x.ProductTag)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .FirstOrDefault(x => x.Id == id);

            if (product == null) return View("NotFoundPage");

            return View(product);
        }


        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.ProductTags.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Tags = _context.ProductTags.ToList();
            ViewBag.Colors = _context.Colors.ToList();

            if (!ModelState.IsValid) return View();

            if (!_context.Categories.Any(x => x.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Have not Category like this name!");
                return View();
            }

            if (!_context.Brands.Any(x => x.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Have not Brand like this name!");
                return View();
            }

            if (_context.Products.Any(x => x.Code == product.Code))
            {
                ModelState.AddModelError("Code", "Code is required!");
                return View();
            }

            if (product.IsOnOffer)
            {
                if (product.DiscountPercent==null)
                {
                    ModelState.AddModelError("DiscountPercent", "Product is on offer must be discount percent!");
                    return View();
                }
            }

            #region ImageChack

            if (product.PosterImage == null)
            {
                ModelState.AddModelError("PosterImage", "Poster image can not be null!");
                return View();
            }

            if (product.HoverPosterImage == null)
            {
                ModelState.AddModelError("PosterImage", "Hover Poster image can not be null!");
                return View();
            }

            if (product.PosterImage.ContentType != "image/jpeg" && product.PosterImage.ContentType != "image/png")
            {
                ModelState.AddModelError("PosterImage", "You can choose file only .jpg, .jpeg or .png format!");
                return View();
            }

            if (product.PosterImage.Length > 5242880)
            {
                ModelState.AddModelError("PosterImage", "You can choose file only maximum 5Mb !");
                return View();
            }

            if (product.HoverPosterImage.ContentType != "image/jpeg" && product.HoverPosterImage.ContentType != "image/png")
            {
                ModelState.AddModelError("HoverPosterImage", "You can choose file only .jpg, .jpeg or .png format!");
                return View();
            }

            if (product.HoverPosterImage.Length > 5242880)
            {
                ModelState.AddModelError("HoverPosterImage", "You can choose file only maximum 5Mb !");
                return View();
            }

            if (product.Images != null)
            {
                foreach (var item in product.Images)
                {
                    if (item.ContentType != "image/jpeg" && item.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Images", "You can choose file only .jpg, .jpeg or .png format!");
                        return View();
                    }

                    if (item.Length > 5242880)
                    {
                        ModelState.AddModelError("Images", "You can choose file only maximum 5Mb !");
                        return View();
                    }
                }
            }

            #endregion


            #region ImageUpload

            product.ProImages = new List<ProImage>();
            product.ProductTagProducts = new List<ProductTagProduct>();
            product.ProductColors = new List<ProductColor>();

            ProImage posterImage = new ProImage()
            {
                PosterStatus = true,
                Image = FileManager.Save(_env.WebRootPath, "uploads/products", product.PosterImage)
            };

            product.ProImages.Add(posterImage);

            ProImage hoverPosterImage = new ProImage()
            {
                PosterStatus = false,
                Image = FileManager.Save(_env.WebRootPath, "uploads/products", product.HoverPosterImage)
            };

            product.ProImages.Add(hoverPosterImage);

            if (product.Images != null)
            {
                foreach (var item in product.Images)
                {
                    ProImage productImage = new ProImage()
                    {
                        PosterStatus = null,
                        Image = FileManager.Save(_env.WebRootPath, "uploads/products", item)
                    };

                    product.ProImages.Add(productImage);
                }
            }

            if (product.TagIds != null)
            {
                foreach (var item in product.TagIds)
                {
                    ProductTagProduct productTag = new ProductTagProduct()
                    {
                        ProductTagId = item
                    };

                    product.ProductTagProducts.Add(productTag);
                }
            }

            if (product.ColorIds != null)
            {
                foreach (var item in product.ColorIds)
                {
                    ProductColor productColor = new ProductColor()
                    {
                        ColorId = item
                    };

                    product.ProductColors.Add(productColor);
                }
            }

            #endregion

            product.Rate = 0;
            if (product.CreatedAt == null)
            {
                product.CreatedAt = DateTime.Now;
            }

            _context.Products.Add(product);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var item in product.ProImages)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/products", item.Image);
                }
            }

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Product product = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.ProductTagProducts)
                .Include(x => x.ProductColors)
                .FirstOrDefault(x => x.Id == id);


            product.TagIds = product.ProductTagProducts.Select(x => x.ProductTagId).ToList();
            product.ColorIds = product.ProductColors.Select(x => x.ColorId).ToList();

            if (product == null) return View("NotFoundPage");

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Tags = _context.ProductTags.ToList();

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Tags = _context.ProductTags.ToList();

            if (!ModelState.IsValid) return View();

            Product existProduct = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.ProductTagProducts)
                .Include(x => x.ProductColors)
                .FirstOrDefault(x => x.Id == product.Id);

            if (existProduct == null) return View("NotFoundPage");

            #region AnyCheck

            if (!_context.Categories.Any(x => x.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Have not Category like this name!");
                return View(existProduct);
            }

            if (!_context.Brands.Any(x => x.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Have not Brand like this name!");
                return View(existProduct);
            }

            if (_context.Products.Any(x => x.Code == product.Code && product.Code != existProduct.Code))
            {
                ModelState.AddModelError("Code", "Code is required!");
                return View(existProduct);
            }

            if (product.IsOnOffer)
            {
                if (product.DiscountPercent == null)
                {
                    ModelState.AddModelError("DiscountPercent", "Product is on offer must be discount percent!");
                    return View();
                }
            }

            #endregion

            #region ImageChack

            if (product.PosterImage != null)
            {
                if (product.PosterImage.ContentType != "image/jpeg" && product.PosterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("PosterImage", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(existProduct);
                }

                if (product.PosterImage.Length > 5242880)
                {
                    ModelState.AddModelError("PosterImage", "You can choose file only maximum 5Mb !");
                    return View(existProduct);
                }
            }

            if (product.HoverPosterImage != null)
            {
                if (product.HoverPosterImage.ContentType != "image/jpeg" && product.HoverPosterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("HoverPosterImage", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(existProduct);
                }

                if (product.HoverPosterImage.Length > 5242880)
                {
                    ModelState.AddModelError("HoverPosterImage", "You can choose file only maximum 5Mb !");
                    return View(existProduct);
                }
            }

            if (product.Images != null)
            {
                foreach (var item in product.Images)
                {
                    if (item.ContentType != "image/jpeg" && item.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Images", "You can choose file only .jpg, .jpeg or .png format!");
                        return View(existProduct);
                    }

                    if (item.Length > 5242880)
                    {
                        ModelState.AddModelError("Images", "You can choose file only maximum 5Mb !");
                        return View(existProduct);
                    }
                }
            }

            #endregion


            existProduct.Name = product.Name;
            existProduct.Desc = product.Desc;
            existProduct.Code = product.Code;
            existProduct.CostPrice = product.CostPrice;
            existProduct.Price = product.Price;
            existProduct.DiscountPercent = product.DiscountPercent;
            existProduct.Weight = product.Weight;
            existProduct.Dimensions = product.Dimensions;
            existProduct.Materials = product.Materials;
            existProduct.OtherInfo = product.OtherInfo;
            existProduct.Count = product.Count;
            existProduct.CreatedAt = product.CreatedAt;
            existProduct.IsOnOffer = product.IsOnOffer;
            existProduct.CategoryId = product.CategoryId;
            existProduct.BrandId = product.BrandId;


            if (product.TagIds != null)
            {
                List<ProductTagProduct> productTags = new List<ProductTagProduct>();
                foreach (var item in product.TagIds)
                {
                    ProductTagProduct productTag = new ProductTagProduct()
                    {
                        ProductTagId = item
                    };
                    productTags.Add(productTag);
                }
                existProduct.ProductTagProducts = productTags;
            }
            else
            {
                existProduct.ProductTagProducts.Clear();
            }

            if (product.ColorIds != null)
            {
                List<ProductColor> productColors = new List<ProductColor>();
                foreach (var item in product.ColorIds)
                {
                    ProductColor productColor = new ProductColor()
                    {
                        ColorId = item
                    };
                    productColors.Add(productColor);
                }
                existProduct.ProductColors = productColors;
            }
            else
            {
                existProduct.ProductColors.Clear();
            }


            #region ImageUpload

            List<string> newFileNames = new List<string>();

            if (product.PosterImage != null)
            {
                string filename = FileManager.Save(_env.WebRootPath, "uploads/products", product.PosterImage);
                ProImage oldPoster = existProduct.ProImages.FirstOrDefault(x => x.PosterStatus == true);

                if (oldPoster != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/products", oldPoster.Image);
                    oldPoster.Image = filename;
                }
                else
                {
                    ProImage productImage = new ProImage()
                    {
                        PosterStatus = true,
                        Image = filename
                    };
                    existProduct.ProImages.Add(productImage);
                }

                newFileNames.Add(filename);

            }

            if (product.HoverPosterImage != null)
            {
                string filename = FileManager.Save(_env.WebRootPath, "uploads/products", product.HoverPosterImage);
                ProImage oldPoster = existProduct.ProImages.FirstOrDefault(x => x.PosterStatus == false);

                if (oldPoster != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/products", oldPoster.Image);
                    oldPoster.Image = filename;
                }
                else
                {
                    ProImage productImage = new ProImage()
                    {
                        PosterStatus = false,
                        Image = filename
                    };
                    existProduct.ProImages.Add(productImage);
                }
                newFileNames.Add(filename);
            }


            if (existProduct.ProImages.FindAll(x => x.PosterStatus == null)!=null)
            {

                if (product.ImageIds != null)
                {
                    foreach (var item in existProduct.ProImages.FindAll(x => x.PosterStatus == null))
                    {
                        if (!product.ImageIds.Any(x => x == item.Id))
                        {
                            FileManager.Delete(_env.WebRootPath, "uploads/products", item.Image);

                            _context.ProImages.Remove(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in existProduct.ProImages.FindAll(x => x.PosterStatus == null))
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/products", item.Image);

                        _context.ProImages.Remove(item);
                    }
                }
            }

            if (product.Images != null)
            {
                foreach (var item in product.Images)
                {
                    string newFileName = FileManager.Save(_env.WebRootPath, "uploads/products", item);

                    ProImage productImage = new ProImage()
                    {
                        Image = newFileName
                    };
                    existProduct.ProImages.Add(productImage);

                    newFileNames.Add(newFileName);
                }
            }

            #endregion

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var item in newFileNames)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/products", item);
                }

                return View("NotFoundPage");
            }

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {

            Product product = _context.Products
                .Include(x => x.ProImages)
                .Include(x => x.ProductTagProducts)
                .Include(x => x.ProductColors)
                .FirstOrDefault(x => x.Id == id);

            if (product == null) return View("NotFoundPage");


            product.TagIds = product.ProductTagProducts.Select(x => x.ProductTagId).ToList();
            product.ColorIds = product.ProductColors.Select(x => x.ColorId).ToList();

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Tags = _context.ProductTags.ToList();

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Tags = _context.ProductTags.ToList();


            Product existProduct = _context.Products.Include(x => x.ProImages).FirstOrDefault(x => x.Id == product.Id);

            if (existProduct == null) return View("NotFoundPage");

            if (existProduct.ProImages != null)
            {
                foreach (var item in existProduct.ProImages)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/products", item.Image);
                }
            }

            _context.Products.Remove(existProduct);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Reviews(int productId)
        {
            if(!_context.Products.Any(x => x.Id == productId)) return View("NotFoundPage");

            ViewBag.ProductId = productId;

            List<Review> reviews = _context.Reviews
                .Include(x=>x.AppUser).ThenInclude(x=>x.Reviews)
                .Where(x => x.ProductId == productId).ToList();

            return View(reviews);
        }

        public IActionResult ReviewAccept(int productId, int id)
        {
            Review review = _context.Reviews.FirstOrDefault(x => x.Id == id && x.ProductId == productId);

            if (review == null) return View("NotFoundPage");

            ViewBag.ProductId = productId;

            foreach (var item in _context.Reviews)
            {
                if (item.AppUserId == review.AppUserId)
                {
                    item.IsAccepting = false;
                }
            }

            review.IsAccepting = true;

            _context.SaveChanges();

            Product product = _context.Products.Include(x => x.Reviews).FirstOrDefault(x => x.Id == review.ProductId);

            var acceptedReviews = product.Reviews.Where(x => x.IsAccepting == true);

            product.Rate = acceptedReviews.Count() == 0d ? 0 : acceptedReviews.Average(x => (double)x.Rate);

            _context.SaveChanges();

            return RedirectToAction("reviews", new { productId = review.ProductId });
        }

        public IActionResult ReviewReject(int productId, int id)
        {
            Review review = _context.Reviews.FirstOrDefault(x => x.Id == id && x.ProductId == productId);

            if (review == null) return View("NotFoundPage");

            ViewBag.ProductId = productId;

            review.IsAccepting = false;
            _context.SaveChanges();

            Product product = _context.Products.Include(x => x.Reviews).FirstOrDefault(x => x.Id == review.ProductId);

            var acceptedReviews = product.Reviews.Where(x => x.IsAccepting == true);

            product.Rate = acceptedReviews.Count() == 0d ? 0 : acceptedReviews.Average(x => (double)x.Rate);

            _context.SaveChanges();

            return RedirectToAction("reviews", new { productId = review.ProductId });
        }
    }
}
