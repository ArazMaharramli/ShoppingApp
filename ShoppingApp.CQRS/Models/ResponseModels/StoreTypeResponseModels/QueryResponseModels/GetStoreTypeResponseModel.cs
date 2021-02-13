using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels
{
    public class GetStoreTypeResponseModel : BaseResponseModel
    {
        public StoreType StoreType { get; set; }
    }
}
