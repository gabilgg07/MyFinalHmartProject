using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class BlogTagBlog
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int BlogTagId { get; set; }

        public Blog Blog { get; set; }

        public BlogTag BlogTag { get; set; }
    }
}
