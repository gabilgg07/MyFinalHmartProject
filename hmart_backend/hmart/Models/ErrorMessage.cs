using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    [BindProperties]
    public class ErrorMessage
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
