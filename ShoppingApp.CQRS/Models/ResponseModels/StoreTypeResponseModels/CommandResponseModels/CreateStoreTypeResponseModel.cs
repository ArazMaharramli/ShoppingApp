using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels
{
    public class CreateStoreTypeResponseModel : BaseResponseModel
    {
        public StoreType StoreType { get; set; }
    }

}
