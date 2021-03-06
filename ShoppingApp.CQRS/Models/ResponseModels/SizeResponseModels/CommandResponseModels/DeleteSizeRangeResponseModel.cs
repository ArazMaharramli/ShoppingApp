using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels
{
    public class DeleteSizeRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}
