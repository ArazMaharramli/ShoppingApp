using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.IdentityCommands
{
    public class LoginCommand : IRequest<LoginAndRegisterCommandsResponseModel>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
