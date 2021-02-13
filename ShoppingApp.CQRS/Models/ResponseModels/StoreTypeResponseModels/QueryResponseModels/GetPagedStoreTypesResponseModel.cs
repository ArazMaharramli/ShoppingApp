using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels
{
    public class GetPagedStoreTypesResponseModel : BaseResponseModel
    {
        public IPagedList<StoreType> StoreTypes { get; set; }
    }
}
