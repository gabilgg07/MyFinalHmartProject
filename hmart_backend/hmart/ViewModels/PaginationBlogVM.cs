using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class PaginationBlogVM
    {
        public int? TagId { get; set; }
        public int SelectedPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalBlogs { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
