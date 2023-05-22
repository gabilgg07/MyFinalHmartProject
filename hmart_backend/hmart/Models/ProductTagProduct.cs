using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class ProductTagProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductTagId { get; set; }
        public ProductTag ProductTag { get; set; }
    }
}
