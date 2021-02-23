using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.Utils.InternalModels;
using System.Collections.Generic;
using ShoppingApp.Utils.Classes;
using ShoppingApp.Utils.Enums;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreCommandHandlers
{
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, CreateStoreResponseModel>
    {
        private readonly IStoreService _service;
        private readonly ICountryService _countryService;
        private readonly IStoreTypeService _storeTypeService;
        private readonly IUserIdentityService _userService;

        public CreateStoreCommandHandler(IStoreService service, ICountryService countryService, IStoreTypeService storeTypeService, IUserIdentityService userService)
        {
            _countryService = countryService;
            _storeTypeService = storeTypeService;
            _service = service;
            _userService = userService;
        }

        public async Task<CreateStoreResponseModel> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.FindByEmailAsync(request.Email);
                if (user is null)
                {
                    var password = PasswordGenerator.GenerateRandomPassword();
                    var result = await _userService.CreateAsync(
                        new User
                        {
                            FirstName = request.Name,
                            LastName = request.Surname,
                            UserName = request.Email,
                            Email = request.Email,
                            PhoneNumber = request.PhoneNumber
                        },
                        password: password);
                    if (!result.Succeeded)
                    {
                        return null;
                    }
                    user = await _userService.FindByEmailAsync(request.Email);
                }

                var storetype = await _storeTypeService.FindByGobalIdAsync(request.StoreTypeId);
                var city = await _countryService.GetCityAsync(request.CityId);
                var contacts = new List<StoreContact>
            {
                new StoreContact
                {
                    ContactType = ContactType.Email,
                    Value = request.StoreEmail
                },
                new StoreContact
                {
                    ContactType=ContactType.Phone,
                    Value=request.StorePhone
                }
            };
                var address = new Address
                {
                    City = city,
                    AddressLine1 = request.Address,
                    ZipCode = request.ZipCode
                };
                var users = new List<User> { user };

                return new CreateStoreResponseModel
                {
                    Store = await _service.CreateStoreAsync(
                    storeName: request.StoreName,
                    storeSlug: request.StoreSlug,
                    storeStatus: request.StoreStatus,
                    storeType: storetype,
                    storeContacts: contacts,
                    storeAddress: address,
                    users: users)
                };
            }
            catch (System.Exception ex)
            {
                return new CreateStoreResponseModel
                {
                    HasError = true,
                    ErrorType = ErrorType.Exception,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = ex.Message
                        }
                    }
                };
            }
        }
    }
}
