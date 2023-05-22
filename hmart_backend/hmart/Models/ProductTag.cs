using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class ProductTag
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        public List<ProductTagProduct> ProductTagProducts { get; set; }

        public int Order { get; set; }
    }
}
