using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class PagenationVM
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? ColorId { get; set; }
        public int? TagId { get; set; }
        public string SearchName { get; set; }
        public int SelectedPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }
        public List<Product> Products { get; set; }

        public string SortingBy { get; set; }
    }
}
