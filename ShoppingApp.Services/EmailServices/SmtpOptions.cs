using System;
namespace ShoppingApp.Services.EmailServices
{
    public class SmtpOptions
    {
        public SmtpOptions(string smtpPassword, string smtpUserEmai, string smtpHost, int smtpPort)
        {
            SmtpPassword = smtpPassword;
            SmtpUserEmail = smtpUserEmai;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
        }

        public string SmtpPassword { get; set; }
        public string SmtpUserEmail { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}
