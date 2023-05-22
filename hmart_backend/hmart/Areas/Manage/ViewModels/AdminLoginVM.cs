using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
