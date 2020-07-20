using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomAuthenticationSampleProject.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthenticationSampleProject.Controllers
{
     [CheckPermission]
     [Authorize]
    public class BaseController : Controller
    {
      
       
    }
}
