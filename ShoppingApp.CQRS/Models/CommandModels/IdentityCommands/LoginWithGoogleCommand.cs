using System;
using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.IdentityCommands
{
    public class LoginWithGoogleCommand : IRequest<ExternalLoginCommandsResponseModel>
    {
        public string Token { get; }
        public UserType UserType { get; set; }
        public LoginWithGoogleCommand(string token, UserType userType)
        {
            Token = token;
            UserType = userType;
        }
    }
}
