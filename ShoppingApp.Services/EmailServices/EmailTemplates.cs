using System;
namespace ShoppingApp.Services.EmailServices
{
    public class EmailTemplates
    {
        public string Welcome { get; set; } = "Hello,{0}. \nWe are glad to see you here.";
        public string WelcomeConfirmEmail { get; set; } = "Hello,{0}. \nWe are glad to see you here.\n<a href=\"{1}\">here</a> to confirm your email.";
        public string ForgotPassword { get; set; } = "Hello,{0}.\nClick <a href=\"{1}\">here</a> to reset your password \n";

    }
}
