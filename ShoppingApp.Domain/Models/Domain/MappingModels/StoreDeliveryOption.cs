using System;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MappingModels
{
    public class StoreDeliveryOption : BaseEntity<Status>
    {
        public long DeliveryOptionId { get; set; }
        public DeliveryOption DeliveryOption { get; set; }

        public long StoreId { get; set; }
        public Store Store { get; set; }
    }
}
