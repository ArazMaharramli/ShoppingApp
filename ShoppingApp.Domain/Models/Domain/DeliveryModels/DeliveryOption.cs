using System;
using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Domain.Models.Domain.OrderModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.DeliveryModels
{
    public class DeliveryOption : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }
        public string Description { get; set; }

        public ICollection<StoreDeliveryOption> StoreDeliveryOptions { get; set; } = new HashSet<StoreDeliveryOption>();
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }

}
