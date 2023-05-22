using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class BlogComment
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 600)]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public List<BlogSubComment> BlogSubComments { get; set; }
    }
}
