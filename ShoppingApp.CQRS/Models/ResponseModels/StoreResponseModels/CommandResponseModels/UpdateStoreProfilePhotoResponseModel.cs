using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels
{
    public class UpdateStoreProfilePhotoResponseModel : BaseResponseModel
    {
        public string PhotoUrl { get; set; }
    }
}
