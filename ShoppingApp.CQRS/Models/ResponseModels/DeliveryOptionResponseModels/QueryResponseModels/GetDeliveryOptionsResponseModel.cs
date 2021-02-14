using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels
{
    public class GetDeliveryOptionsResponseModel : BaseResponseModel
    {
        public IEnumerable<DeliveryOption> DeliveryOptions { get; set; }
    }
}
