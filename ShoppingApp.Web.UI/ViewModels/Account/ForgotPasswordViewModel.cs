using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
