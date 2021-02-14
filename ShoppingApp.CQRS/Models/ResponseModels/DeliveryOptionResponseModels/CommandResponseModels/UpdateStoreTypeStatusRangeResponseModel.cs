using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels
{
    public class UpdateDeliveryOptionStatusRangeResponseModel : BaseResponseModel
    {
        public List<DeliveryOption> DeliveryOptions { get; set; }
    }
}
