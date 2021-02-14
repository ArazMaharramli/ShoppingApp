using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.IdentityCommands
{
    public class ForgotPasswordCommand : IRequest<ForgotAndResetPasswordResponseModel>
    {
        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
