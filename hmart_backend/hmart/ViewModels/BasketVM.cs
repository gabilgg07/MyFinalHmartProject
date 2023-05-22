using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }

        public bool IsAddBtn { get; set; }
    }
}
