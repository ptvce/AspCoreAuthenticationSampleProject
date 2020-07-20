using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthenticationSampleProject.Controllers.Base
{
    public class CityController : BaseController
    {
        public IActionResult Index()
        {
            return View("~/Views/Base/City/Index.cshtml");
        }
        public IActionResult Details()
        {
            return View("~/Views/Base/City/Index.cshtml");
        }
    }
}
