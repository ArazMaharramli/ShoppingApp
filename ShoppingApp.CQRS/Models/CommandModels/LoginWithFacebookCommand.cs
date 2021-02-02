using System;
using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels
{
    public class LoginWithFacebookCommand : IRequest<ExternalLoginCommandsResponseModel>
    {
        public string Token { get; }

        public LoginWithFacebookCommand(string token)
        {
            Token = token;
        }
    }
}
