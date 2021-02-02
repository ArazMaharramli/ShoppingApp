using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.PaymentModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MappingModels
{
    public class StorePaymentOption : BaseEntity<Status>
    {
        public long StoreId { get; set; }
        public Store Store { get; set; }

        public long PaymentOptionId { get; set; }
        public PaymentOption PaymentOption { get; set; }

    }
}

