using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationSampleProject.Models
{
    public class UsersViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Thumbnail { get; set; } //HttpPostedFileBase  from view to action
        public string ThumbnailBase64 { get; set; }  //from action to view
        public string ThumbnailUrl { get; set; }
    }
}
