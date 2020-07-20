using CustomAuthenticationSampleProject.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using CustomAuthenticationSampleProject.Entities;

namespace CustomAuthenticationSampleProject.Common
{
    public class CheckPermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int roleId = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Role.ToString()).Value);
            string areaName = context.RouteData.Values["area"] != null ? context.RouteData.Values["area"].ToString() : "";
            string controllerName = context.RouteData.Values["controller"].ToString();
            string actionName = context.RouteData.Values["action"].ToString();
            string actionType = context.HttpContext.Request.Method;

            var dbContext = context.HttpContext.RequestServices.GetService<CoreLearningCustomAuthenticationContext>();
            if (dbContext.Permission.Any(q => q.RolePermissions.Any(r => r.RoleId == roleId) &&
                q.ActionName == actionName && q.ControllerName == controllerName && q.AreaName == areaName))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller" , "Error" },
                    { "action" , "Index"}
                });
            }
        }
    }
}
