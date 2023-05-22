using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.ViewModels
{
    public class RoleCreateVM
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string Name { get; set; }
    }
}
