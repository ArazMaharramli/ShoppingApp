using System;
using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels
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
