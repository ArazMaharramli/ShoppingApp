using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Domain.Models.Domain.OrderModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.PaymentModels
{
    public class PaymentOption : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }

        public string IconUrl { get; set; }

        public ICollection<StorePaymentOption> StorePaymentOptions { get; set; } = new HashSet<StorePaymentOption>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
