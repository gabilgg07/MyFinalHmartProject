using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class AboutVM
    {
        public Setting Setting { get; set; }
        public List<Team> Teams { get; set; }
        public List<Feature> Features { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
