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
        [HttpPost]
        public IActionResult Login(LoginSignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Users.Where(e => e.UserName == model.UserName).SingleOrDefault();
                if(data!=null)
                {
                    bool isValid = (data.UserName == model.UserName && data.Password == model.Password);
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
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");

        }
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
                    Password=model.Password,
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
