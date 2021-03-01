using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.FileServices;
using ShoppingApp.Utils.Classes;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreCommandHandlers
{
    public class UpdateStoreProfileCommandHandler : IRequestHandler<UpdateStoreProfileCommand, UpdateStoreResponseModel>
    {
        private readonly IStoreService _storeService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICountryService _countryService;

        public UpdateStoreProfileCommandHandler(IStoreService storeService, IFileService fileService, IWebHostEnvironment webHostEnvironment, ICountryService countryService)
        {
            _storeService = storeService;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _countryService = countryService;
        }

        public async Task<UpdateStoreResponseModel> Handle(UpdateStoreProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _storeService.FindByOwnerIdAsync(request.OwnerId);
                if (store is null)
                {
                    return generateErrorResponse("Store Not Found.", ErrorType.Model);
                }
                if (!(request.ProfilePictureFile is null))
                {
                    store.ProfilePhotoUrl = await _fileService.UploadFileAsync(request.ProfilePictureFile, _webHostEnvironment.WebRootPath, $"Stores/{store.UniqueSlug}", $"{store.UniqueSlug}_ProfilePhoto");
                }

                if (store.UniqueSlug != request.StoreSlug)
                {
                    if (!(await _storeService.IsSlugAvailable(request.StoreSlug)))
                    {
                        return generateErrorResponse(errorMessage: "slug is not available", ErrorType.Model);
                    }
                }

                var city = await _countryService.GetCityAsync(request.SelectedCityId);
                if (city is null)
                {
                    return generateErrorResponse(errorMessage: "City is not available", ErrorType.Model);
                }
                var emailContact = store.StoreContacts.Where(x => x.ContactType == ContactType.Email && x.Status == Status.Active).FirstOrDefault();
                var phoneNumberContact = store.StoreContacts.Where(x => x.ContactType == ContactType.Phone && x.Status == Status.Active).FirstOrDefault();
                if (emailContact.Value != request.Email.Trim().ToLower())
                {
                    emailContact.Status = Status.Deleted;
                    emailContact.UpdatedDate = LocalTime.Now();
                    store.StoreContacts.Add(new Domain.Models.Domain.StoreModels.StoreContact { ContactType = ContactType.Email, Value = request.Email });
                }
                if (phoneNumberContact.Value != request.PhoneNumber.Trim().ToLower())
                {
                    phoneNumberContact.Status = Status.Deleted;
                    phoneNumberContact.UpdatedDate = LocalTime.Now();
                    store.StoreContacts.Add(new Domain.Models.Domain.StoreModels.StoreContact { ContactType = ContactType.Phone, Value = request.PhoneNumber });
                }
                store.Description = request.Description;
                store.UniqueSlug = request.StoreSlug;
                store.Address.AddressLine1 = request.AddressLine;
                store.Address.ZipCode = request.ZipCode;
                store.Address.City = city;

                await _storeService.UpdateStoreAsync(store);
                return new UpdateStoreResponseModel
                {
                    Store = store
                };
            }
            catch (System.Exception ex)
            {
                return generateErrorResponse(ex.Message, ErrorType.Exception);
            }
        }
        #region MyRegion
        private UpdateStoreResponseModel generateErrorResponse(string errorMessage, ErrorType errorType)
        {
            return new UpdateStoreResponseModel
            {
                HasError = true,
                ErrorType = errorType,
                Errors = new List<InternalErrorModel>
                {
                    new InternalErrorModel
                    {
                        Message = errorMessage
                    }
                }
            };
        }
        #endregion
    }
}
