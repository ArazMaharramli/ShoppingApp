using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

                await _emailSender.SendEmailAsync(userInDb.Email, "Welcome", $"Hello,{user.FirstName}. We are glad to see you here.");
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
}
