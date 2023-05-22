using hmart.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class OrderCreateVM
    {
        public Setting Setting { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string Phone { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string Country { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string StateOrRegion { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string ZipOrPostalCode { get; set; }

        [Required]
        [StringLength(maximumLength: 125)]
        public string Address { get; set; }

        [StringLength(maximumLength: 250)]
        public string Note { get; set; }

        public bool? IsExpress { get; set; }

        public double? ShippingPrice { get; set; }

        public List<BasketItem> BasketItems { get; set; }
    }
}
