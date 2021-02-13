using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels
{
    public class UpdateStoreTypeStatusRangeResponseModel : BaseResponseModel
    {
        public List<StoreType> StoreTypes { get; set; }
    }
}
