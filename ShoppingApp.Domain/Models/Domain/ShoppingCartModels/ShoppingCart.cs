using System;
using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ShoppingCartModels
{
    public class ShoppingCart : BaseEntitySimple<Status>
    {
        public ShoppingCartType ShoppingCardType { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
    }
}
