using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginAndRegisterCommandsResponseModel>
    {
        private readonly IUserIdentityService _userIdentityService;

        public LoginCommandHandler(
            IUserIdentityService userIdentityService
            )
        {
            _userIdentityService = userIdentityService;
        }

        public async Task<LoginAndRegisterCommandsResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userIdentityService.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new LoginAndRegisterCommandsResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>{
                        new InternalErrorModel{
                            Message="Email or password is incorrect"
                        }
                    }
                };
            }

            if (!user.LockoutEnabled)
            {
                var passwordVerified = await _userIdentityService.CheckPasswordAsync(user, request.Password);

                if (passwordVerified)
                {
                    return new LoginAndRegisterCommandsResponseModel
                    {
                        User = user
                    };
                }
                return new LoginAndRegisterCommandsResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>() {
                        new InternalErrorModel {
                        Message = "Password is incorrect"
                    } },
                };
            }

            return new LoginAndRegisterCommandsResponseModel
            {
                HasError = true,
                ErrorType = Utils.Enums.ErrorType.Model,
                Errors = new List<InternalErrorModel> {
                    new InternalErrorModel{
                        Message = "You are not allowed to log in"
                    }
                }
            };
        }
    }
}
