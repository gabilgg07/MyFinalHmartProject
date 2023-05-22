using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class BlogSubComment
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 100)]
        public string Subject { get; set; }

        [StringLength(maximumLength: 600)]
        public string Message { get; set; }

        public DateTime? Date { get; set; }

        public int BlogCommentId { get; set; }

        public BlogComment BlogComment { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
