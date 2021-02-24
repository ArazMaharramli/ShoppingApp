using System;
namespace ShoppingApp.Services.EmailServices
{
    public class EmailTemplates
    {
        public string Welcome { get; set; } = "Hello,{0}. \nWe are glad to see you here.";
        public string WelcomeConfirmEmail { get; set; } = "Hello,{0}. \nWe are glad to see you here.\n<a href=\"{1}\">here</a> to confirm your email.";
        public string ForgotPassword { get; set; } = "Hello,{0}.\nClick <a href=\"{1}\">here</a> to reset your password \n";
        public string StoreCreated { get; set; } = "Hello,{0}.\n Your Store is created. Please Click <a href=\"{1}\">here</a> to set your password and login for next steps.\n";
        public string StoreBlocked { get; set; } = "Helo, {0}. \nDue to some reasons  your store is blocked by site admin. Please contact with our support center for more detailed info.";
    }
}
