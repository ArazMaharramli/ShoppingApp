using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels
{
    public class DeleteColorRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}