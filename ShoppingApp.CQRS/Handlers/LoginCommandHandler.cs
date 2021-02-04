using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginAndRegisterCommandsResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentityService _userIdentityService;

        public LoginCommandHandler(
            DbContextOptions<ShoppingAppDbContext> contextOptions,
            IUserIdentityService userIdentityService,
            UserManager<User> userManager)
        {
            _unitOfWork = new UnitOfWork(new ShoppingAppDbContext(contextOptions));
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
                    Errors = new List<InternalErrorModel>{
                        new InternalErrorModel{
                            Type = Utils.Enums.ErrorType.Model,
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
                    Errors = new List<InternalErrorModel>() {
                        new InternalErrorModel {
                        Type = Utils.Enums.ErrorType.Model,
                        Message = "Verify your password for login"
                    } },
                };
            }



            return new LoginAndRegisterCommandsResponseModel
            {
                HasError = true,
                Errors = new List<InternalErrorModel> {
                    new InternalErrorModel{
                        Type = Utils.Enums.ErrorType.Model,
                        Message = "You are not allowed to log in"
                    }
                }
            };
        }
    }
}
