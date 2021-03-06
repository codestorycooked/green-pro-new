﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenPro.WebClient.Models
{

    public class ExternalLoginConfirmationViewModel : GreenPro.Data.AspNetUser
    {
        [Required]
        [Display(Name = "User name (Email)")]
        public string UserName { get; set; }
        [Display(Name = "State")]
        [Required]
        public int StateId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> StateList { get; set; }
        [Display(Name = "City")]
        [Required]
        
        public int CityId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> CityList { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string provider { get; set; }
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : GreenPro.Data.AspNetUser
    {
        [Required]
        [Display(Name = "User name (Email)")]
        [EmailAddress(ErrorMessage = "Please Enter your email ID")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "State")]
        [Required]
        public int StateId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> StateList { get; set; }
        [Display(Name = "City")]
        [Required]
        //public string City { get; set; }
        public int CityId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> CityList { get; set; }


        public int? DateOfBirthDay { get; set; }
        
        public int? DateOfBirthMonth { get; set; }
        
        public int? DateOfBirthYear { get; set; }



    }


    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}