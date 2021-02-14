using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels
{
    public class DeleteDeliveryOptionRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}
