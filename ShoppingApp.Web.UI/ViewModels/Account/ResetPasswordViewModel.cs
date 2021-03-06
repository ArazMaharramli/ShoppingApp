﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
