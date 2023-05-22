using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.ViewModels
{
    public class RoleVM
    {
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
