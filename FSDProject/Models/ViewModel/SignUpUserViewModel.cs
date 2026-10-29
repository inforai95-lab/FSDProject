using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FSDProject.Models.ViewModel
{
    public class SignUpUserViewModel
    {
     
        public int Id { get; set; }
        [Required(ErrorMessage ="Pleas Enter UserName")]
        [Remote(action: "UserNameIsExist", controller: "Account")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Pleas Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pleas Enter Mobile")]
        [Display(Name ="Mobile Number")]
        [Remote(action: "MobileIsExist", controller:"Account")]
        public long? Mobile { get; set; }
        [Required(ErrorMessage = "Pleas Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Pleas Enter ConfirmPassword")]
        [Compare("Password", ErrorMessage =("Confirm Password Can't Matched!"))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="Active")]
        public bool IsActive { get; set; }
   
    }
}
