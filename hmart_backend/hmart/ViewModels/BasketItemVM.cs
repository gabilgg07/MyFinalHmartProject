using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class BasketItemVM
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
