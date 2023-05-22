using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class HomeVM
    {
        public Setting Setting { get; set; }
        public List<Slider> Sliders { get; set; }
        public Banner MainBanner { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Product> NewProducts { get; set; }
        public List<Product> TopRatedProducts { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public Offer MainOffer { get; set; }
        public List<Offer> Offers { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
