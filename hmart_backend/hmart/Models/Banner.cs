﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class Banner
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        public double Price { get; set; }

        public bool IsMain { get; set; }

        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int? Order { get; set; }
    }
}
