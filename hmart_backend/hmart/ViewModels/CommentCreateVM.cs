using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class CommentCreateVM
    {
        public int BlogId { get; set; }

        [StringLength(maximumLength: 250)]
        public string Message { get; set; }
    }
}
