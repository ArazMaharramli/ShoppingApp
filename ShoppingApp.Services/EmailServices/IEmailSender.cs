using System.Threading.Tasks;

namespace ShoppingApp.Services.EmailServices
{
    public interface IEmailSender
    {
        Task SendWelcomeEmailAsync(string email, string subject, string userName, string confirmEmailUrl);
        Task SendResetPasswordEmailAsync(string email, string subject, string userName, string resetPasswordUrl);
    }
}
