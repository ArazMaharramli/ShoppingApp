using MediatR;
using Microsoft.AspNetCore.Http;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreCommands
{
    public class UpdateStoreProfilePhotoCommand : IRequest<UpdateStoreProfilePhotoResponseModel>
    {
        public UpdateStoreProfilePhotoCommand(
            string ownerId,
            IFormFile profilePictureFile)
        {

            ProfilePictureFile = profilePictureFile;
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
    }
}
