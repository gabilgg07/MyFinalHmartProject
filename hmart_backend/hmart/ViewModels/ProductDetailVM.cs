using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class ProductDetailVM
    {
        public Setting Setting { get; set; }
        public Product Product { get; set; }

        public AddReviewVM AddReviewVM { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}
