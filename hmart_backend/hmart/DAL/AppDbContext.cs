using hmart.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.DAL
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductTagProduct> ProductTagProducts { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogTagBlog> BlogTagBlogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogSubComment> BlogSubComments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProImage> ProImages { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
