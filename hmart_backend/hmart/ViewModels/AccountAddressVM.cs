using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.ViewModels
{
    public class AccountAddressVM
    {

        [StringLength(maximumLength: 100)]
        public string Address { get; set; }

        [StringLength(maximumLength: 25)]
        public string City { get; set; }

        [StringLength(maximumLength: 50)]
        public string StateOrRegion { get; set; }

        [StringLength(maximumLength: 25)]
        public string ZipOrPostalCode { get; set; }

        [StringLength(maximumLength: 25)]
        public string Country { get; set; }
    }
}
