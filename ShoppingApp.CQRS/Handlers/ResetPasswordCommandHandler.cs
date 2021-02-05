using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ForgotAndResetPasswordResponseModel>
    {
        private readonly IUserIdentityService _userIdentityService;

        public ResetPasswordCommandHandler(IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService;
        }
        public async Task<ForgotAndResetPasswordResponseModel> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userIdentityService.FindByIdAsync(request.UserId);
            if (!(user == null))
            {
                if (user.Email == request.Email)
                {
                    var result = await _userIdentityService.ResetPasswordAsync(user, request.Code, request.Password);
                    if (result.Succeeded)
                    {
                        return new ForgotAndResetPasswordResponseModel
                        {
                            Message = "Password updated."
                        };
                    }

                    var errors = result.Errors.Select(x =>
                        new InternalErrorModel
                        {
                            Message = x.Description,
                        });
                    return new ForgotAndResetPasswordResponseModel
                    {
                        HasError = true,
                        ErrorType = Utils.Enums.ErrorType.Model,
                        Errors = errors
                    };
                }

                return new ForgotAndResetPasswordResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Email does not match. Enter valid email address"
                        }
                    }
                };



            }
            return new ForgotAndResetPasswordResponseModel { HasError = true };
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link

        }
    }

}
