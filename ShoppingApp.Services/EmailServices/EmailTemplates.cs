using System;
namespace ShoppingApp.Services.EmailServices
{
    public class EmailTemplates
    {
        public string Welcome { get; set; } = "Hello,{0}. We are glad to see you here.\n<a href=\"{1}\">here</a> to confirm your email.";
        public string ForgotPassword { get; set; } = "Hello,{0}.\nClick <a href=\"{0}\">here</a> to reset your password \n";
    }
}
