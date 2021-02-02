using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.AuthServices.FacebookAuthService;
using ShoppingApp.Services.AuthServices.JwtTokenServices;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class LoginWithFacebookCommandHandler : IRequestHandler<LoginWithFacebookCommand, ExternalLoginCommandsResponseModel>
    {
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentityService _userIdentityService;

        public LoginWithFacebookCommandHandler(
            IFacebookAuthService facebookAuthService,
            UserManager<User> userManager,
            DbContextOptions<ShoppingAppDbContext> contextOptions,
            IUserIdentityService userIdentityService)
        {
            _facebookAuthService = facebookAuthService;
            _userManager = userManager;
            _unitOfWork = new UnitOfWork(new ShoppingAppDbContext(contextOptions));
            _userIdentityService = userIdentityService;
        }

        public async Task<ExternalLoginCommandsResponseModel> Handle(LoginWithFacebookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInfoFromFacebook = await _facebookAuthService.ValidateTokenAndGetUserInfoAsync(request.Token);
                if (userInfoFromFacebook != null)
                {
                    if (userInfoFromFacebook.Error != null)
                    {
                        // write error to db with Unique id

                        return ReturnError(error: userInfoFromFacebook.Error);
                    }

                    var userType = await _unitOfWork.UserTypes.GetAsync(x => x.UniqueName == "Customer");

                    if (userType == null)
                    {
                        return ReturnError(error: new InternalErrorModel
                        {
                            Type = Utils.Enums.ErrorType.Server,
                            Message = "User Type is null"
                        });
                    }
                    var userInDb = await _userManager.FindByEmailAsync(userInfoFromFacebook.Email);
                    //db-da check edib varsa userin datasini almaq ve login etmek sonra jwt yaratmaq
                    var user = await _userManager.FindByLoginAsync("Facebook", userInfoFromFacebook.Id);


                    var loginInfo = new UserLoginInfo("Facebook", userInfoFromFacebook.Id, null);
                    if (userInDb != null)
                    {
                        var loginresult = await _userManager.AddLoginAsync(userInDb, loginInfo);
                        if (loginresult.Succeeded)
                        {

                            return ReturnSuccess(user);
                        }
                        else
                        {
                            return ReturnError(loginresult);
                        }
                    }

                    if (user == null)
                    {

                        user = new User
                        {
                            Email = userInfoFromFacebook.Email,
                            FirstName = userInfoFromFacebook.FirstName,
                            LastName = userInfoFromFacebook.LastName,
                            UserName = userInfoFromFacebook.Email,
                            UserTypeId = userType.Id,
                            ProfilePhoto = userInfoFromFacebook.PictureUrl,
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
                        else
                        {
                            return ReturnError(result);
                        }
                    }
                    //

                    //token model

                    return ReturnSuccess(user);
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
            }


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
