using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public bool IsMain { get; set; }


        [StringLength(maximumLength: 100)]
        public string Predecessor { get; set; }

        [StringLength(maximumLength: 100)]
        public string SupporType { get; set; }

        [StringLength(maximumLength: 100)]
        public string Cushioning { get; set; }

        public DateTime OfferTime { get; set; }

        public int Order { get; set; }
    }
}
