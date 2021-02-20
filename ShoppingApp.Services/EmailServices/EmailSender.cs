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

        public async Task SendResetPasswordEmailAsync(string email, string userName, string resetPasswordUrl)
        {
            var message = string.Format((new EmailTemplates()).ForgotPassword, userName, resetPasswordUrl);
            await SendEmailAsync(email, "Reset Password", message);
        }

        public async Task SendWelcomeEmailAsync(string email, string userName)
        {
            var message = string.Format((new EmailTemplates()).Welcome, userName);
            await SendEmailAsync(email, "Welcome", message);
        }

        public async Task SendWelcomeConfirmEmailAsync(string email, string userName, string confirmEmailUrl)
        {
            var message = string.Format((new EmailTemplates()).WelcomeConfirmEmail, userName, confirmEmailUrl);
            await SendEmailAsync(email, "Welcome", message);
        }

        public async Task SendStoreCreatedEmailAsync(string email, string userName, string resetPasswordUrl)
        {
            var message = string.Format((new EmailTemplates()).StoreCreated, userName, resetPasswordUrl);
            await SendEmailAsync(email, "Store Created", message);
        }
        #region privates
        private async Task SendEmailAsync(string email, string subject, string message)
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
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_options.Value.SmtpHost, _options.Value.SmtpPort, SecureSocketOptions.Auto);
                    client.Authenticate(_options.Value.EmailAddress, _options.Value.SmtpPassword);
                    client.Send(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch (System.Exception ex)
                {

                }
            }
        }
        #endregion
    }
}
