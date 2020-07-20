using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomAuthenticationSampleProject.Common;
using CustomAuthenticationSampleProject.Data;
using CustomAuthenticationSampleProject.Entites;
using CustomAuthenticationSampleProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthenticationSampleProject.Controllers
{
    public class AccountController : Controller
    {
        CoreLearningCustomAuthenticationContext _context;
        public AccountController(CoreLearningCustomAuthenticationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        #region Login

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //check username already exist
            //check password difficulty
            //user isactive

            var user = _context.Users.SingleOrDefault(q => q.UserName == model.UserName);
            var saltPassword = user.PasswordSalt;
            var hashPassword = EncryptionUtility.HashPasswordWithSalt(model.Password, saltPassword.ToString());

            if (user.Password != hashPassword)
            {
                ModelState.AddModelError("", "invalid password");
                return View(model);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim("FullName", model.UserName),
                    new Claim(ClaimTypes.Role, "1"), // read from db user role table
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //if username exist
            //check password and confirmpassword
            //get saltpassword from user table

            var saltPassword = Guid.NewGuid();
            var hashPassword = EncryptionUtility.HashPasswordWithSalt(model.Password, saltPassword.ToString());

            var user = new Users
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                UserName = model.UserName,
                Password = hashPassword,
                PasswordSalt = saltPassword
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Login));

        }
        #endregion
    }
}
