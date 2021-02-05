using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.AuthServices.GoogleAuthService;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, ExternalLoginCommandsResponseModel>
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailSender _emailSender;

        public LoginWithGoogleCommandHandler(
            IGoogleAuthService googleAuthService,
            IUserIdentityService userIdentityService,
            IEmailSender emailSender)
        {
            _googleAuthService = googleAuthService;
            _userIdentityService = userIdentityService;
            _emailSender = emailSender;
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
                        return ReturnError(errorList: userInfoFromGoogle.Errors.ToList(), errorType: ErrorType.Server);
                    }

                    var userInDb = await _userIdentityService.FindByEmailAsync(userInfoFromGoogle.Email);

                    var loginInfo = new UserLoginInfo("Google", userInfoFromGoogle.Id, null);
                    if (userInDb != null)
                    {
                        var loginresult = await _userIdentityService.AddLoginAsync(userInDb, loginInfo);
                        if (loginresult.Succeeded)
                        {

                            return ReturnSuccess(userInDb);
                        }
                        else
                        {
                            return ReturnError(result: loginresult, errorType: ErrorType.Model);
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
                            UserType = request.UserType,
                            ProfilePhoto = userInfoFromGoogle.PictureUrl,
                        };

                        var userContact = new UserContact
                        {
                            ContactType = ContactType.Email,
                            Value = userInfoFromGoogle.Email
                        };

                        user.UserContacts.Add(userContact);

                        var result = await _userIdentityService.CreateAsync(user);

                        if (result.Succeeded)
                        {
                            var addLoginResult = await _userIdentityService.AddLoginAsync(user, loginInfo);
                            if (addLoginResult.Succeeded)
                            {
                                await _emailSender.SendWelcomeEmailAsync(userInDb.Email, user.FirstName);
                                return ReturnSuccess(user);
                            }
                            else
                            {
                                return ReturnError(result: addLoginResult, errorType: ErrorType.Model);
                            }
                        }

                        return ReturnError(result: result, errorType: ErrorType.Model);

                    }
                }
                return ReturnError(
                    error: new InternalErrorModel
                    {
                        Message = "Not Authorized"
                    },
                  errorType: ErrorType.Model);
            }
            catch (Exception ex)
            {
                return ReturnError(error: new InternalErrorModel
                {
                    Message = ex.Message
                },
                errorType: ErrorType.Exception);
            };

        }


        #region Helpers
        private ExternalLoginCommandsResponseModel ReturnError(ErrorType errorType, IdentityResult result = null, InternalErrorModel error = null, List<InternalErrorModel> errorList = null)
        {
            var errors = new List<InternalErrorModel>();
            if (error != null)
            {
                errors.Add(error);
            }

            foreach (var item in result.Errors)
            {
                errors.Add(new InternalErrorModel
                {
                    Message = item.Description
                });
            }
            errors.AddRange(errorList);
            return new ExternalLoginCommandsResponseModel
            {
                HasError = true,
                ErrorType = errorType,
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
