using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels
{
    public class GetStoreTypesResponseModel : BaseResponseModel
    {
        public IEnumerable<StoreType> StoreTypes { get; set; }
    }
}
