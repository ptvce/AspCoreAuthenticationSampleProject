using CustomAuthenticationSampleProject.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationSampleProject.Entities
{
    public  class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

    }
    public class Permission
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public ActionType ActionType  { get; set; }
        public bool ShowInMenu { get; set; }
        public string Url { get; set; }
        public ICollection<RolePermission>  RolePermissions { get; set; }
    }

    public class RolePermission
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public virtual  Permission Permission { get; set; }

        public int RoleId { get; set; }
        public virtual Role IdentityRole { get; set; }
    }

    public class UserRole
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual Users User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role{ get; set; }
    }
    public enum ActionType 
    {
        GET = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4
    }
}
