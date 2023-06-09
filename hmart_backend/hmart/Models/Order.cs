﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool? IsExpress { get; set; }

        public double? ShippingPrice { get; set; }

        [StringLength(maximumLength: 35)]
        public string Country { get; set; }

        [StringLength(maximumLength: 35)]
        public string City { get; set; }

        [StringLength(maximumLength: 35)]
        public string StateOrRegion { get; set; }

        [StringLength(maximumLength: 25)]
        public string ZipOrPostalCode { get; set; }

        [StringLength(maximumLength: 250)]
        public string Address { get; set; }

        [StringLength(maximumLength: 25)]
        public string Phone { get; set; }

        [StringLength(maximumLength: 250)]
        public string Note { get; set; }

        [StringLength(maximumLength: 250)]
        public string AdminNote { get; set; }

        public double TotalPrice { get; set; }
        public bool? Status { get; set; }
        public AppUser AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
