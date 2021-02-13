using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels
{
    public class DeleteStoreTypeRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}
