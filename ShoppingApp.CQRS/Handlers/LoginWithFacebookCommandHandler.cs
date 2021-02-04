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
using ShoppingApp.Services.AuthServices.FacebookAuthService;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class LoginWithFacebookCommandHandler : IRequestHandler<LoginWithFacebookCommand, ExternalLoginCommandsResponseModel>
    {
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailSender _emailSender;

        public LoginWithFacebookCommandHandler(
            IFacebookAuthService facebookAuthService,
            UserManager<User> userManager,
            DbContextOptions<ShoppingAppDbContext> contextOptions,
            IUserIdentityService userIdentityService, IEmailSender emailSender)
        {
            _facebookAuthService = facebookAuthService;
            _unitOfWork = new UnitOfWork(new ShoppingAppDbContext(contextOptions));
            _userIdentityService = userIdentityService;
            _emailSender = emailSender;
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

                    var userInDb = await _userIdentityService.FindByEmailAsync(userInfoFromFacebook.Email);

                    var loginInfo = new UserLoginInfo("Facebook", userInfoFromFacebook.Id, null);
                    if (userInDb != null)
                    {
                        var loginresult = await _userIdentityService.AddLoginAsync(userInDb, loginInfo);
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
                            Email = userInfoFromFacebook.Email,
                            FirstName = userInfoFromFacebook.FirstName,
                            LastName = userInfoFromFacebook.LastName,
                            UserName = userInfoFromFacebook.Email,
                            UserType = request.UserType,
                            ProfilePhoto = userInfoFromFacebook.PictureUrl,
                        };

                        var userContact = new UserContact
                        {
                            ContactType = Utils.Enums.ContactType.Email,
                            Value = userInfoFromFacebook.Email
                        };

                        user.UserContacts.Add(userContact);

                        var result = await _userIdentityService.CreateAsync(user);

                        if (result.Succeeded)
                        {
                            var addLoginResult = await _userIdentityService.AddLoginAsync(user, loginInfo);
                            if (addLoginResult.Succeeded)
                            {
                                await _emailSender.SendEmailAsync(userInDb.Email, "Welcome", $"Hello,{user.FirstName}. We are glad to see you here.");
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
