using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class AccountDetailVM
    {
        [StringLength(maximumLength: 25)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 125)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(maximumLength: 25)]
        public string Phone { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        public IFormFile ImageFile { get; set; }

        [BindProperty]
        public string Gender { get; set; }
        public string[] Genders = new[] { "Mr.", "Mrs." };


        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }
    }
}
