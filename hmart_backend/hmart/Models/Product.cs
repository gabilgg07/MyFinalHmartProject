using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [StringLength(maximumLength: 800)]
        public string Desc { get; set; }

        [StringLength(maximumLength: 50)]
        public string Code { get; set; }

        public int Count { get; set; }
        public double CostPrice { get; set; }
        public double Price { get; set; }
        public int? DiscountPercent { get; set; }
        public double? Rate { get; set; }

        [StringLength(maximumLength: 50)]
        public string Weight { get; set; }

        [StringLength(maximumLength: 50)]
        public string Dimensions { get; set; }

        [StringLength(maximumLength: 100)]
        public string Materials { get; set; }

        [StringLength(maximumLength: 200)]
        public string OtherInfo { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public bool IsOnOffer { get; set; }


        [NotMapped]
        public IFormFile PosterImage { get; set; }

        [NotMapped]
        public IFormFile HoverPosterImage { get; set; }

        [NotMapped]
        public List<IFormFile> Images { get; set; }

        [NotMapped]
        public List<int> ImageIds { get; set; }

        [NotMapped]
        public List<int> TagIds { get; set; }

        [NotMapped]
        public List<int> ColorIds { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }


        public List<BasketItem> BasketItems { get; set; }
        public List<WishListItem> WishListItems { get; set; }
        public List<ProImage> ProImages { get; set; }
        public List<ProductTagProduct> ProductTagProducts { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<Review> Reviews { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public string GetPosterImage()
        {
            return ProImages.FirstOrDefault(x => x.PosterStatus == true).Image;
        }

        public string GetHoverPosterImage()
        {
            return ProImages.FirstOrDefault(x => x.PosterStatus == false).Image;
        }

        public List<string> GetImages()
        {
            List<string> images = new List<string>();

            foreach (var item in ProImages.Where(x=>x.PosterStatus==null))
            {
                images.Add(item.Image);
            }

            return images;
        }
    }
}
