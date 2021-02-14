using System;
using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.IdentityCommands
{
    public class LoginWithFacebookCommand : IRequest<ExternalLoginCommandsResponseModel>
    {
        public string Token { get; }
        public UserType UserType { get; set; }
        public LoginWithFacebookCommand(string token, UserType userType)
        {
            Token = token;
            UserType = userType;
        }
    }
}
