using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels
{
    public class GetDeliveryOptionResponseModel : BaseResponseModel
    {
        public DeliveryOption DeliveryOption { get; set; }
    }
}
