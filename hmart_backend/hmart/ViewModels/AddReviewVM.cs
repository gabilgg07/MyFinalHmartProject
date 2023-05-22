using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class AddReviewVM
    {
        public int ProductId { get; set; }

        [StringLength(maximumLength: 250)]
        public string Message { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
    }
}
