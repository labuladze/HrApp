
using HrApp.Data;
using HrApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDbContext _context;
        private readonly IData _data;

        public HomeController(UserDbContext context, IData data)
        {
            _context = context;
            _data = data;
        }

        //Get: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        //Get: Home/profile
        public IActionResult Profile()
        {
            var Name = HttpContext.Session.GetString("Name");
            var Email = HttpContext.Session.GetString("Email");
            if (Name == null && Email == null)
            {
                return RedirectToAction("Login");
            }
            var info = _data.GetAll();
            return View(info);
        }

        #region Login 
        public IActionResult Login()
        {
            var Name = HttpContext.Session.GetString("Name");
            if (Name == null )
            {
                return View();     
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var email = user.Email;
            var passw = Md5(user.Password);
            var res = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == passw);
            if (res != null)
            {   
                HttpContext.Session.SetString("Email",email);
                HttpContext.Session.SetString("Name", res.Name);
                return RedirectToAction("Profile");
            }
            ViewBag.Notification = "Email or Password is Incorrect";
            return View();
        }

        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        #endregion

        #region Registration 
        // Get: /Home/Register
        public IActionResult Register()
        {
            return View();
        }

        // Post: /Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Md5(user.Password);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");

            }
            return View();
        }
        #endregion 

        #region Email Verification 
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEmail(string email)
        {
            var res =   _context.Users.FirstOrDefault(opt => opt.Email == email);
            if(res == null)
            {
                return Json(true);
            }
            return Json($"The Email {email} is in use");
        }
        #endregion

        #region Md5 Hashing
        public string Md5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes).Replace("-", "");
            var result = password.ToLower();
            return result;
        }
        #endregion
    }
}
