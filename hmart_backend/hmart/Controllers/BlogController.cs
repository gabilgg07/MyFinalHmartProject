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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int? tagId)
        {
            var query = _context.Blogs.AsQueryable();

            if (tagId != null)
            {
                query = query.Where(x => x.BlogTagBlogs.Any(x => x.BlogTagId == tagId));
            }

            int totalBlogs = query.Count();

            BlogVM blogVM = new BlogVM
            {
                Setting = _context.Settings.FirstOrDefault(),

                SelectedPage = 1,
                TotalPages = (int)Math.Ceiling(totalBlogs / 9d),
                TotalBlogs = totalBlogs,
                TagId = tagId,
                Blogs = query.Take(9).ToList()
            };

            return View(blogVM);
        }

        public IActionResult Pagenation(int? tagId, int page = 1)
        {
            var query = _context.Blogs.AsQueryable();

            if (tagId != null)
            {
                query = query.Where(x => x.BlogTagBlogs.Any(x => x.BlogTagId == tagId));
            }

            int totalBlogs = query.Count();

            PaginationBlogVM paginationBlogVM = new PaginationBlogVM
            {
                SelectedPage = page,
                TotalPages = (int)Math.Ceiling(totalBlogs / 9d),
                TotalBlogs = totalBlogs,
                TagId = tagId,
                Blogs = query.Include(x=>x.BlogTagBlogs).ThenInclude(x=>x.BlogTag)
                .Skip((page-1)*9).Take(9).ToList()
            };

            return PartialView("_BlogsPartial", paginationBlogVM);
        }

        public IActionResult Detail(int id)
        {
            Blog blog = _context.Blogs
                .Include(x=>x.BlogTagBlogs).ThenInclude(x=>x.BlogTag)
                .Include(x=>x.BlogComments).ThenInclude(x=>x.AppUser)
                .Include(x=>x.BlogComments).ThenInclude(x=>x.BlogSubComments).ThenInclude(x=>x.AppUser)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null) return View("NotFound");

            BlogDetailVM blogDetailVM = new BlogDetailVM
            {
                Setting = _context.Settings.FirstOrDefault(),
                Blog = blog,
                BeforeBlog = _context.Blogs.FirstOrDefault(x=>x.Date<blog.Date),
                AfterBlog = _context.Blogs.FirstOrDefault(x=>x.Date>blog.Date)
            };

            return View(blogDetailVM);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Addcomment(CommentCreateVM commentCreateVM)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid)
                return RedirectToAction("detail", "blog", new { id = commentCreateVM.BlogId });

            Blog blog = _context.Blogs.Include(x => x.BlogComments).FirstOrDefault(x => x.Id == commentCreateVM.BlogId);
            if (blog == null)
                return View("NotFound");

            BlogComment blogComment = new BlogComment
            {
                BlogId = commentCreateVM.BlogId,
                AppUserId = user.Id,
                Message = commentCreateVM.Message,
                Date = DateTime.UtcNow
            };
            blog.BlogComments.Add(blogComment);

            _context.SaveChanges();
            return RedirectToAction("detail", "blog", new { id = commentCreateVM.BlogId });
        }
    }
}
