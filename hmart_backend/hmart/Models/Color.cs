using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class Color
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        [StringLength(maximumLength: 10)]
        public string Code { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public int Order { get; set; }
    }
}
