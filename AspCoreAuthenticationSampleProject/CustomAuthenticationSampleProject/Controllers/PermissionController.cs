using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomAuthenticationSampleProject.Common;
using CustomAuthenticationSampleProject.Data;
using CustomAuthenticationSampleProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthenticationSampleProject.Controllers
{
    public class PermissionController : Controller
    {
        CoreLearningCustomAuthenticationContext _dbcontext;
        public PermissionController(CoreLearningCustomAuthenticationContext dbContext) 
        {
            _dbcontext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var permission = await _dbcontext.Permission.Select(q => new PermissionViewModel
            {
                Id = q.Id,
                ActionName = q.ActionName,
                ActionType = q.ActionType,
                AreaName = q.AreaName,
                ControllerName = q.ControllerName,
                ShowInMenu = q.ShowInMenu,
                Title = q.Title,
                Url = q.Url,
                EncryptedId = EncryptionUtility.Encrypt(q.Id.ToString())

            }).ToListAsync();
            //var permission = await _dbcontext.Permission.ToListAsync();
            return View(permission);
        }
        public async Task<IActionResult> Details(string id)
        {
            var permissionId = Convert.ToUInt32(EncryptionUtility.Decrypt(id));
            var permission = await _dbcontext.Permission.SingleOrDefaultAsync(q => q.Id == permissionId);
            return View(permission);
        }
    }
}
