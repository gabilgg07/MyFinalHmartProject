using hmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class ShopVM
    {
        public Setting Setting { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? ColorId { get; set; }
        public int? TagId { get; set; }
        public string SearchName { get; set; }
        public int SelectedPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
