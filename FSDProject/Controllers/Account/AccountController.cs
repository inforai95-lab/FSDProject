using FSDProject.Data;
using FSDProject.Models.Account;
using FSDProject.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSDProject.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        public AccountController(ApplicationContext context)
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
        public IActionResult UserNameIsExist(string userName)
        {
            var data = _context.Users.Where(e => e.UserName == userName).SingleOrDefault();
            if (data != null)
            {
                return Json($"UserName {userName} already exists in users");
            }
            else
            {
                return Json(true);
            }
        }
        [AcceptVerbs("Post","Get")]
        public IActionResult MobileIsExist(long? mobile)
        {
            var data = _context.Users.Where(e => e.Mobile == mobile).SingleOrDefault();
            if (data != null)
            {
                return Json($"Mobile {mobile} already exists in users");
            }
            else
            {
                return Json(true);
            }
        }
        [HttpPost]
        public IActionResult Login(LoginSignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Users.Where(e => e.UserName == model.UserName).SingleOrDefault();
                if(data!=null)
                {
                    bool isValid = (data.UserName == model.UserName && DecryptPassword(data.Password) == model.Password);
                    if(isValid)
                    {
                        var identity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, model.UserName) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("UserName", model.UserName);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorpassword"] = "Invalid password!";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUserName"] = "UserName not found!";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
           
        }
        public static string EncryptPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
           
        }
        public  static string  DecryptPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");

        }
       // [AcceptVerbs("Post","Get")]
    
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var data = new User()
                {
                    UserName=model.UserName,
                    Email=model.Email,
                    Mobile=model.Mobile,
                    Password=EncryptPassword(model.Password),
                    IsActive=model.IsActive
                };
                _context.Users.Add(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "you are eligible to Login";
                return RedirectToAction("Login");
            }           
            else
            {
                TempData["errorMessage"] = "Emty Form can't be Submitted";
                return View(model);
            }
           
        }
    }
}
