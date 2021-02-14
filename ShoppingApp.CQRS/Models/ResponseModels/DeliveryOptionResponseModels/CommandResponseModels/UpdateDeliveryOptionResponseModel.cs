﻿using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels
{
    public class UpdateDeliveryOptionResponseModel : BaseResponseModel
    {
        public DeliveryOption DeliveryOption { get; set; }
    }
}
