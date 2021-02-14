using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels
{
    public class GetPagedDeliveryOptionsResponseModel : BaseResponseModel
    {
        public IPagedList<DeliveryOption> DeliveryOptions { get; set; }
    }
}
