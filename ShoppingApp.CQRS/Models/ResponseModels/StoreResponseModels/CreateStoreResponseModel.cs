using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels
{
    public class CreateStoreResponseModel : BaseResponseModel
    {
        public Store Store { get; set; }
    }
}
