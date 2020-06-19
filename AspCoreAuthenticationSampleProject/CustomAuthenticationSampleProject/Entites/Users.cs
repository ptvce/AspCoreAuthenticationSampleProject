using System;
using System.Collections.Generic;

namespace CustomAuthenticationSampleProject.Entites
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid PasswordSalt { get; set; }
        public bool IsActive { get; set; }
    }
}
