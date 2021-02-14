using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.IdentityCommands
{
    public class RegisterCommand : IRequest<LoginAndRegisterCommandsResponseModel>
    {
        public RegisterCommand(string firstName, string lastName, string email, string password, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            UserType = userType;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
