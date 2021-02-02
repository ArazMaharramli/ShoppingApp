using System;
using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels
{
    public class LoginWithGoogleCommand : IRequest<ExternalLoginCommandsResponseModel>
    {
        public string Token { get; }

        public LoginWithGoogleCommand(string token)
        {
            Token = token;
        }
    }
}
