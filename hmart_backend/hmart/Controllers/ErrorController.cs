using hmart.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.errorMessage = "Page Not Found";
                    break;
            }

            return View("NotFound");
        }

        [HttpGet("details")]
        public IActionResult HttpStatusCodeHandler(ErrorMessage errorM)
        {
            switch (errorM.Code)
            {
                case 404:
                    ViewBag.errorMessage = errorM.Name +" by Id: \""+errorM.Id+ "\" not found";
                    break;
            }

            return View("NotFound");
        }
    }
}
