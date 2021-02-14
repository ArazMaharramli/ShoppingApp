using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.IdentityCommands;
using ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.IdentityCommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginAndRegisterCommandsResponseModel>
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailSender _emailSender;

        public RegisterCommandHandler(
            IUserIdentityService userIdentityService,
            IEmailSender emailSender)
        {
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
                        ErrorType = Utils.Enums.ErrorType.Model,
                        Errors = new List<InternalErrorModel>
                        {
                            new InternalErrorModel
                           {
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
                    LockoutEnabled = false
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
                        ErrorType = Utils.Enums.ErrorType.Model,
                        Errors = createUserResult.Errors.Select(x =>
                        new InternalErrorModel
                        {
                            Message = x.Description
                        }).ToList()
                    };
                }

                var code = await _userIdentityService.GenerateEmailConfirmationTokenAsync(user);
                var encodedcode = HttpUtility.UrlEncode(code);
                var link = $"https://localhost:5005/account/ConfirmEmail?userId={user.Id}&code={encodedcode}";
                await _emailSender.SendWelcomeConfirmEmailAsync(user.Email, user.FirstName, link);
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
                    ErrorType = Utils.Enums.ErrorType.Exception,
                    Errors = new List<InternalErrorModel> {
                        new InternalErrorModel{
                            Message = ex.InnerException.Message
                        },
                    },
                };
            }
        }
    }
}
