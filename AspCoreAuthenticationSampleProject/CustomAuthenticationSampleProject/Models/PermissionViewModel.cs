using CustomAuthenticationSampleProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationSampleProject.Models
{
    public class PermissionViewModel
    {
        public string EncryptedId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public ActionType ActionType { get; set; }
        public bool ShowInMenu { get; set; }
        public string Url { get; set; }
    }
}
