using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.ViewModels.Account
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
