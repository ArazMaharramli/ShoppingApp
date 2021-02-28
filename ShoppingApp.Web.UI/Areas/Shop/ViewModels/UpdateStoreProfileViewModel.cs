using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels
{
    public class UpdateStoreProfileViewModel
    {
        public string ProfilePhotoUrl { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }

        public IFormFile ProfilePhoto { get; set; }
        public string StoreSlug { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public UpdateAddressViewModel Address { get; set; }

    }
}
