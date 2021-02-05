using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

    }
}
