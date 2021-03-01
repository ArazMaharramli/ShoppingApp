using MediatR;
using Microsoft.AspNetCore.Http;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreCommands
{
    public class UpdateStoreProfileCommand : IRequest<UpdateStoreResponseModel>
    {
        public UpdateStoreProfileCommand(
            string ownerId, IFormFile profilePictureFile,
            string storeSlug, string description,
            string email, string phoneNumber,
            string facebookUrl, string instagramUrl,
            string zipCode, string selectedCityId, string addressLine)
        {
            OwnerId = ownerId;
            ProfilePictureFile = profilePictureFile;
            StoreSlug = storeSlug;
            Description = description;
            Email = email;
            PhoneNumber = phoneNumber;
            FacebookUrl = facebookUrl;
            InstagramUrl = instagramUrl;
            ZipCode = zipCode;
            SelectedCityId = selectedCityId;
            AddressLine = addressLine;
        }

        public string OwnerId { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
        public string StoreSlug { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }

        public string ZipCode { get; set; }
        public string SelectedCityId { get; set; }
        public string AddressLine { get; set; }

    }
}
