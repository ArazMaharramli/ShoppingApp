using System;
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
using ShoppingApp.Services.AuthServices.GoogleAuthService;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, ExternalLoginCommandsResponseModel>
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentityService _userIdentityService;

        public LoginWithGoogleCommandHandler(
            IGoogleAuthService googleAuthService,
            UserManager<User> userManager,
            DbContextOptions<ShoppingAppDbContext> contextOptions,
            IUserIdentityService userIdentityService)
        {
            _googleAuthService = googleAuthService;
            _userManager = userManager;
            _unitOfWork = new UnitOfWork(new ShoppingAppDbContext(contextOptions));
            _userIdentityService = userIdentityService;
        }

        public async Task<ExternalLoginCommandsResponseModel> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var userInfoFromGoogle = await _googleAuthService.ValidateTokenAndGetUserInfo(request.Token);
                if (userInfoFromGoogle != null)
                {
                    if (userInfoFromGoogle.HasError)
                    {
                        return ReturnError(error: userInfoFromGoogle.Error);
                    }

                    var userType = await _unitOfWork.UserTypes.GetAsync(x => x.UniqueName == "Customer");

                    if (userType == null)
                    {
                        return ReturnError(error: new InternalErrorModel
                        {
                            Type = Utils.Enums.ErrorType.Model,
                            Message = "User Type is null"
                        });
                    }

                    var userInDb = await _userManager.FindByEmailAsync(userInfoFromGoogle.Email);

                    var loginInfo = new UserLoginInfo("Google", userInfoFromGoogle.Id, null);
                    if (userInDb != null)
                    {
                        var loginresult = await _userManager.AddLoginAsync(userInDb, loginInfo);
                        if (loginresult.Succeeded)
                        {

                            return ReturnSuccess(userInDb);
                        }
                        else
                        {
                            return ReturnError(loginresult);
                        }
                    }
                    else
                    {

                        var user = new User
                        {
                            Email = userInfoFromGoogle.Email,
                            FirstName = userInfoFromGoogle.FirstName,
                            LastName = userInfoFromGoogle.LastName,
                            UserName = userInfoFromGoogle.Email,
                            UserTypeId = userType.Id,
                            ProfilePhoto = userInfoFromGoogle.PictureUrl,
                        };

                        var result = await _userManager.CreateAsync(user);

                        if (result.Succeeded)
                        {
                            var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                            if (addLoginResult.Succeeded)
                            {

                                return ReturnSuccess(user);
                            }
                            else
                            {
                                return ReturnError(addLoginResult);
                            }
                        }

                        return ReturnError(result: result);

                    }
                }
                return ReturnError(error: new InternalErrorModel
                {
                    Type = Utils.Enums.ErrorType.Model,
                    Message = "Not Authorized"
                });
            }
            catch (Exception ex)
            {
                return ReturnError(error: new InternalErrorModel
                {
                    Type = Utils.Enums.ErrorType.Exception,
                    Message = ex.Message
                });
            };

        }


        #region Helpers
        private ExternalLoginCommandsResponseModel ReturnError(IdentityResult result = null, InternalErrorModel error = null)
        {
            if (error != null)
            {
                return new ExternalLoginCommandsResponseModel
                {
                    HasError = true,
                    Errors = new List<InternalErrorModel> { error }
                };
            }

            var errors = new List<InternalErrorModel>();

            foreach (var item in result.Errors)
            {
                errors.Add(new InternalErrorModel
                {
                    Type = Utils.Enums.ErrorType.Model,
                    Message = item.Description
                });
            }

            return new ExternalLoginCommandsResponseModel
            {
                HasError = false,
                Errors = errors
            };
        }

        private ExternalLoginCommandsResponseModel ReturnSuccess(User user)
        {
            return new ExternalLoginCommandsResponseModel
            {
                User = user,
            };

        }
        #endregion
    }
}
