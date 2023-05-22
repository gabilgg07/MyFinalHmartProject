using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class WishListVM
    {
        public Setting Setting  { get; set; }
        public List<Product> Products { get; set; }
        public int Count { get; set; }

        public bool IsAddBtn { get; set; }
    }
}
