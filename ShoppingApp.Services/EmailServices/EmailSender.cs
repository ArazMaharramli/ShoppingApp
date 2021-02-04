using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace ShoppingApp.Services.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SmtpOptions> _options;



        public EmailSender(IOptions<SmtpOptions> options)
        {
            _options = options;
        }



        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("ShoppingApp", _options.Value.EmailAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            bodyBuilder.TextBody = message;

            emailMessage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_options.Value.SmtpHost, _options.Value.SmtpPort, SecureSocketOptions.Auto);
                client.Authenticate(_options.Value.EmailAddress, _options.Value.SmtpPassword);
                client.Send(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
