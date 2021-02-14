using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.IdentityCommands
{
    public class ResetPasswordCommand : IRequest<ForgotAndResetPasswordResponseModel>
    {
        public ResetPasswordCommand(string email, string password, string code, string userId)
        {
            Email = email;
            Password = password;
            Code = code;
            UserId = userId;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
