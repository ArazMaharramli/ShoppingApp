using System.Threading.Tasks;

namespace ShoppingApp.Services.EmailServices
{
    public interface IEmailSender
    {
        Task SendWelcomeEmailAsync(string email, string userName);
        Task SendWelcomeConfirmEmailAsync(string email, string userName, string confirmEmailUrl);
        Task SendResetPasswordEmailAsync(string email, string userName, string resetPasswordUrl);
        Task SendStoreCreatedEmailAsync(string email, string userName, string resetPasswordUrl);
        Task SendStoreBlockedEmailAsync(string email, string userName);
    }
}
