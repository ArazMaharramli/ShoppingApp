using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels
{
    public class UpdateProfilePhotoViewModel
    {
        public IFormFile ProfilePhoto { get; set; }
    }
}
