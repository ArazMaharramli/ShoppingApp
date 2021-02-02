using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ShoppingApp.Services.EmailServices
{
    public class EmailSender : IEmailSender
    {
        //private readonly IOptions<SmtpConfig> _smtpConfig;



        //public EmailSender(IOptions<SmtpConfig> smtpConfig)
        //{
        //    _smtpConfig = smtpConfig;
        //}



        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //var emailMessage = new MimeMessage();
            //emailMessage.From.Add(new MailboxAddress("ShoppingApp", _smtpConfig.Value.SmtpUserEmail));
            //emailMessage.To.Add(new MailboxAddress("", email));
            //emailMessage.Subject = subject;
            //var bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = message;
            //bodyBuilder.TextBody = message;

            //emailMessage.Body = bodyBuilder.ToMessageBody();
            //using (var client = new SmtpClient())
            //{
            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //    await client.ConnectAsync(_smtpConfig.Value.SmtpHost, _smtpConfig.Value.SmtpPort, SecureSocketOptions.Auto);
            //    client.Authenticate(_smtpConfig.Value.SmtpUserEmail, _smtpConfig.Value.SmtpPassword);
            //    client.Send(emailMessage);
            //    await client.DisconnectAsync(true);
            //}
        }
    }
}
