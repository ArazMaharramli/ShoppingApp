﻿using System;
namespace ShoppingApp.Services.EmailServices
{
    public class SmtpOptions
    {
        //public SmtpOptions(string smtpPassword, string emailAddress, string smtpHost, int smtpPort)
        //{
        //    SmtpPassword = smtpPassword;
        //    EmailAddress = emailAddress;
        //    SmtpHost = smtpHost;
        //    SmtpPort = smtpPort;
        //}

        public string SmtpPassword { get; set; }
        public string EmailAddress { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}
