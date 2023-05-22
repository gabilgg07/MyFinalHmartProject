using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class BlogDetailVM
    {
        public Setting Setting { get; set; }

        public Blog Blog { get; set; }

        public Blog BeforeBlog { get; set; }
        public Blog AfterBlog { get; set; }
        public CommentCreateVM CommentCreateVM { get; set; }
    }
}
