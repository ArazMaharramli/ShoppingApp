using System;
using System.Collections;
using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.PaymentModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.OrderModels
{
    public class Order : BaseEntitySimple<Status>
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public long PaymentOptionId { get; set; }
        public PaymentOption PaymentOption { get; set; }

        // catdirilma adresi
        public long DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
