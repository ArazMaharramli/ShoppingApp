using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels
{
    public class UpdateStoreStatusResponseModel : BaseResponseModel
    {
        public Store Store { get; set; }
    }
}
