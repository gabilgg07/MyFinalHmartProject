using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class BlogTag
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        public List<BlogTagBlog> BlogTagBlogs { get; set; }
        public int Order { get; set; }
    }
}
