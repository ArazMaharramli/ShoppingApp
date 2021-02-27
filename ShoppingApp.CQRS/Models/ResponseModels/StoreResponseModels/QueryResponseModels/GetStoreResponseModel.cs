using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.QueryResponseModels
{
    public class GetStoreResponseModel : BaseResponseModel
    {
        public Store Store { get; set; }
    }
}
