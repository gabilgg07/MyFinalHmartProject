using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.ViewModels
{
    public class AdminDeleteVM
    {
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string Role { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 125)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
