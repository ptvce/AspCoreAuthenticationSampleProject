using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomAuthenticationSampleProject.Data;
using CustomAuthenticationSampleProject.Entites;
using Microsoft.AspNetCore.Identity;
using CustomAuthenticationSampleProject.Models;
using CustomAuthenticationSampleProject.Common;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CustomAuthenticationSampleProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly CoreLearningCustomAuthenticationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(CoreLearningCustomAuthenticationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Download(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(q => q.Id == id);

            if (user == null) return null;
            if (user.Thumbnail == null) return null;

            return File(user.Thumbnail, user.ThumbnailFileExtention);

        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            var result = await _context.Users.ToListAsync();
            var users = new List<UsersViewModel>();

            foreach (var item in result)
            {
                var user = new UsersViewModel();
                user.Id = item.Id;
                user.UserName = item.UserName;
                user.IsActive = item.IsActive;
                //null checker is so important
                user.ThumbnailBase64 = item.Thumbnail != null ? Convert.ToBase64String(item.Thumbnail) : string.Empty;
                user.ThumbnailUrl = $"/Files/{item.ThumbnailUrl}";
                users.Add(user);
            }
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,IsActive,Thumbnail")] UsersViewModel users) // فیلدهایی که در تگ بایندینگ می گذاریم را فقط می فرستیم برای بکند 
        {
            //if (ModelState.IsValid)
            //{
            //    users.Id = Guid.NewGuid();
            //    _context.Add(users);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(users);
            MemoryStream stream = new MemoryStream();
            users.Thumbnail.CopyTo(stream);

            var saltPassword = new Guid();
            var hashPassword = EncryptionUtility.HashPasswordWithSalt(users.Password, saltPassword.ToString());
            string newFileName = $"{Guid.NewGuid()}.{ Path.GetExtension(users.Thumbnail.FileName)}";  //generate new file name

            var user = new Users { 
                Id = Guid.NewGuid(),
                UserName = users.UserName,
                Password = hashPassword,
                PasswordSalt = saltPassword,
                IsActive = users.IsActive,
                //save file in db
                Thumbnail = stream.ToArray(),
                ThumbnailFileExtention = users.Thumbnail.ContentType,
                ThumbnailUrl = newFileName
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            //save file in disk
            //string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files");
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", newFileName);
            using (var fileStream = new FileStream(filePath , FileMode.Create))
            {
                await users.Thumbnail.CopyToAsync(fileStream);
            }

          

            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,Password,PasswordSalt,IsActive,Thumbnail,ThumbnailUrl")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
