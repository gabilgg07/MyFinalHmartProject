using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(maximumLength: 50)]
        public string FisrtName { get; set; }

        [StringLength(maximumLength: 50)]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }

        [BindProperty]
        public string Gender { get; set; }
        public string[] Genders = new[] { "Mr.", "Mrs." };


        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string ZipOrPostalCode { get; set; }
        public string Country { get; set; }

        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public List<BlogComment> BlogComments { get; set; }
        public List<BlogSubComment> BlogSubComments { get; set; }
        public List<Review> Reviews { get; set; }

        public List<BasketItem> BasketItems { get; set; }
        public List<WishListItem> WishListItems { get; set; }

    }
}
