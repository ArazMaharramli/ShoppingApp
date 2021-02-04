using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginAndRegisterCommandsResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailSender _emailSender;

        public RegisterCommandHandler(
            DbContextOptions<ShoppingAppDbContext> contextOptions,
            IUserIdentityService userIdentityService,
            IEmailSender emailSender)
        {
            _unitOfWork = new UnitOfWork(new ShoppingAppDbContext(contextOptions));
            _userIdentityService = userIdentityService;
            _emailSender = emailSender;
        }

        public async Task<LoginAndRegisterCommandsResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInDb = await _userIdentityService.FindByEmailAsync(request.Email);
                if (userInDb != null)
                {
                    return new LoginAndRegisterCommandsResponseModel
                    {
                        HasError = true,
                        Errors = new List<InternalErrorModel>
                        {
                           new InternalErrorModel
                           {
                                Type = Utils.Enums.ErrorType.Model,
                                Message = "Existing User"
                           }
                        }
                    };
                }

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Email,
                    UserType = request.UserType,
                };
                var userContact = new UserContact
                {
                    ContactType = Utils.Enums.ContactType.Email,
                    Value = request.Email
                };

                user.UserContacts.Add(userContact);

                var createUserResult = await _userIdentityService.CreateAsync(user, request.Password);
                if (!createUserResult.Succeeded)
                {
                    return new LoginAndRegisterCommandsResponseModel
                    {
                        HasError = true,
                        Errors = createUserResult.Errors.Select(x =>
                        new InternalErrorModel
                        {
                            Type = Utils.Enums.ErrorType.Model,
                            Message = x.Description
                        }).ToList()
                    };
                }

                await _emailSender.SendWelcomeEmailAsync(user.Email, "Welcome", user.FirstName, "no-link");
                return new LoginAndRegisterCommandsResponseModel
                {
                    User = user,
                    HasError = false
                };
            }
            catch (Exception ex)
            {
                return new LoginAndRegisterCommandsResponseModel
                {
                    HasError = true,
                    Errors = new List<InternalErrorModel> {
                    new InternalErrorModel{
                        Type = Utils.Enums.ErrorType.Exception,
                        Message = ex.InnerException.Message
                    }
                    }
                };
            }
        }
    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotAndResetPasswordResponseModel>
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordCommandHandler(IUserIdentityService userIdentityService, IEmailSender emailSender)
        {
            _userIdentityService = userIdentityService;
            _emailSender = emailSender;
        }
        public async Task<ForgotAndResetPasswordResponseModel> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userIdentityService.FindByEmailAsync(request.Email);
            if (!(user == null || !(await _userIdentityService.IsEmailConfirmedAsync(user))))
            {
                var code = await _userIdentityService.GeneratePasswordResetTokenAsync(user);
                var callBackUrl = $"https://localhost:5003/Account/ResetPassword?userId={user.Id}&code={code}";
                await _emailSender.SendResetPasswordEmailAsync(request.Email, "Forgot Password", user.FirstName, callBackUrl);


                return new ForgotAndResetPasswordResponseModel
                {
                    Message = "Go to your email can click the link"
                };
            }
            return new ForgotAndResetPasswordResponseModel { HasError = true };
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link

        }
    }
}
