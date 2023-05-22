using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class ProImage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:150)]
        public string Image { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public bool? PosterStatus { get; set; }
    }
}
