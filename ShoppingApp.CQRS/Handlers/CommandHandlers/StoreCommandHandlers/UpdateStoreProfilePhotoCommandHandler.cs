using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.FileServices;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreCommandHandlers
{
    public class UpdateStoreProfilePhotoCommandHandler : IRequestHandler<UpdateStoreProfilePhotoCommand, UpdateStoreProfilePhotoResponseModel>
    {
        private readonly IStoreService _storeService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateStoreProfilePhotoCommandHandler(IStoreService storeService, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _storeService = storeService;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<UpdateStoreProfilePhotoResponseModel> Handle(UpdateStoreProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _storeService.FindByOwnerIdAsync(request.OwnerId);
                if (store is null)
                {
                    return generateErrorResponse("Store Not Found.", ErrorType.Model);
                }
                if (request.ProfilePictureFile is null)
                {
                    return generateErrorResponse("File select a file.", ErrorType.Model);
                }

                var pictureUrl = await _fileService.UploadFileAsync(request.ProfilePictureFile, _webHostEnvironment.WebRootPath, $"Stores/{store.UniqueSlug}", $"{store.UniqueSlug}_ProfilePhoto");
                await _storeService.UpdateProfilePhotoAsync(request.OwnerId, pictureUrl);
                return new UpdateStoreProfilePhotoResponseModel
                {
                    PhotoUrl = pictureUrl
                };
            }
            catch (System.Exception ex)
            {
                return generateErrorResponse(ex.Message, ErrorType.Exception);
            }
        }
        #region MyRegion
        private UpdateStoreProfilePhotoResponseModel generateErrorResponse(string errorMessage, ErrorType errorType)
        {
            return new UpdateStoreProfilePhotoResponseModel
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
