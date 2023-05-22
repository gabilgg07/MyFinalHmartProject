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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.ToList();

            return View(blogs);
        }

        public IActionResult Detail(int id)
        {
            Blog blog = _context.Blogs
               .Include(x => x.BlogTagBlogs).ThenInclude(x => x.BlogTag)
               .FirstOrDefault(x => x.Id == id);

            if (blog == null) return View("NotFoundPage");

            return View(blog);
        }

        public IActionResult Create()
        {
            ViewBag.Tags = _context.BlogTags.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            ViewBag.Tags = _context.BlogTags.ToList();

            if (!ModelState.IsValid) return View(blog);

            blog.BlogTagBlogs = new List<BlogTagBlog>();

            if (blog.TagIds == null)
            {
                ModelState.AddModelError("TagIds", "You must select at least one tag!");
                return View();
            }

            foreach (var item in blog.TagIds)
            {
                BlogTagBlog blogTag = new BlogTagBlog()
                {
                    BlogTagId = item
                };

                blog.BlogTagBlogs.Add(blogTag);
            }

            #region ImageChack

            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Blog image can not be null!");
                return View();
            }

            if (blog.ImageFile.ContentType != "image/jpeg" && blog.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                return View();
            }

            if (blog.ImageFile.Length > 5242880)
            {
                ModelState.AddModelError("ImageFile", "You can choose file only maximum 5Mb !");
                return View();
            }

            if (blog.TextImageFile != null)
            {
                if (blog.TextImageFile.ContentType != "image/jpeg" && blog.TextImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("TextImageFile", "You can choose file only .jpg, .jpeg or .png format!");
                    return View();
                }

                if (blog.TextImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("TextImageFile", "You can choose file only maximum 5Mb !");
                    return View();
                }
            }

            #endregion

            blog.Image = FileManager.Save(_env.WebRootPath, "uploads/blogs", blog.ImageFile);

            if (blog.TextImageFile != null)
            {
                blog.TeaxtImage = FileManager.Save(_env.WebRootPath, "uploads/blogs", blog.TextImageFile);
            }

            _context.Blogs.Add(blog);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/blogs", blog.Image);
                FileManager.Delete(_env.WebRootPath, "uploads/blogs", blog.TeaxtImage);
            }

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Blog blog = _context.Blogs
                .Include(x => x.BlogTagBlogs)
                .FirstOrDefault(x => x.Id == id);

            blog.TagIds = blog.BlogTagBlogs.Select(x => x.BlogTagId).ToList();

            if (blog == null) return View("NotFoundPage");

            ViewBag.Tags = _context.BlogTags.ToList();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog)
        {
            ViewBag.Tags = _context.BlogTags.ToList();

            if (!ModelState.IsValid) return View();

            Blog existBlog = _context.Blogs
                .Include(x => x.BlogTagBlogs)
                .FirstOrDefault(x => x.Id == blog.Id);

            if (existBlog == null) return View("NotFoundPage");

            #region ImageChack


            if (blog.ImageFile != null)
            {
                if (blog.ImageFile.ContentType != "image/jpeg" && blog.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (blog.ImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }

            }
            else if (string.IsNullOrWhiteSpace(blog.Image) && !string.IsNullOrWhiteSpace(existBlog.Image))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.Image);

                existBlog.Image = null;
            }

            if (blog.TextImageFile != null)
            {
                if (blog.TextImageFile.ContentType != "image/jpeg" && blog.TextImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz .jpg , .jpeg ve ya .png formatda fayl sece bilersiz!");
                    return View();
                }
                if (blog.TextImageFile.Length > 5242880)
                {
                    ModelState.AddModelError("ImageFile", "Maksimum uzunlugu 5Mb olan fayl sece bilersiz!");
                    return View();
                }
            }
            else if (string.IsNullOrWhiteSpace(blog.TeaxtImage) && !string.IsNullOrWhiteSpace(existBlog.TeaxtImage))
            {

                FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.TeaxtImage);

                existBlog.TeaxtImage = null;
            }

            #endregion

            existBlog.Title = blog.Title;
            existBlog.Date = blog.Date;
            existBlog.Author = blog.Author;
            existBlog.Desc = blog.Desc;
            existBlog.Text = blog.Text;
            existBlog.Quote = blog.Quote;
            existBlog.Quoter = blog.Quoter;

            if (blog.TagIds != null)
            {
                List<BlogTagBlog> blogTags = new List<BlogTagBlog>();
                foreach (var item in blog.TagIds)
                {
                    BlogTagBlog blogTag = new BlogTagBlog()
                    {
                        BlogTagId = item
                    };
                    blogTags.Add(blogTag);
                }
                existBlog.BlogTagBlogs = blogTags;
            }
            else
            {
                existBlog.BlogTagBlogs.Clear();
            }

            #region ImageUpload

            if (blog.ImageFile != null)
            {

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/blogs", blog.ImageFile);

                if (!string.IsNullOrWhiteSpace(existBlog.Image))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.Image);
                }

                existBlog.Image = newFileName;
            }

                if (blog.TextImageFile != null)
            {

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/blogs", blog.TextImageFile);

                if (!string.IsNullOrWhiteSpace(existBlog.TeaxtImage))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.TeaxtImage);
                }

                existBlog.TeaxtImage = newFileName;
            }

            #endregion

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.Image);
                FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.TeaxtImage);
            }

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {

            Blog blog = _context.Blogs
                .Include(x => x.BlogTagBlogs)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null) return View("NotFoundPage");


            blog.TagIds = blog.BlogTagBlogs.Select(x => x.BlogTagId).ToList();

            ViewBag.Tags = _context.BlogTags.ToList();

            return View(blog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Blog blog)
        {
            ViewBag.Tags = _context.BlogTags.ToList();


            Blog existBlog = _context.Blogs.FirstOrDefault(x => x.Id == blog.Id);

            if (existBlog == null) return View("NotFoundPage");

            if (existBlog.Image != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.Image);
            }

            if (existBlog.TeaxtImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/blogs", existBlog.TeaxtImage);
            }


            _context.Blogs.Remove(existBlog);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
