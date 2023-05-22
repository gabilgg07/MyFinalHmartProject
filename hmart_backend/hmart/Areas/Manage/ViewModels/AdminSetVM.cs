using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.ViewModels
{
    public class AdminSetVM
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string FirstName { get; set; }

        [Required]
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
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
