using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.OrderModels
{
    public class OrderItem : BaseEntitySimple<OrderStatus>
    {
        public int Quantity { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }

        public long ProductDetailId { get; set; }
        public ProductDetail ProductDetail { get; set; }

        public long DeliveryOptionId { get; set; }
        public DeliveryOption DeliveryOption { get; set; }

        public ICollection<OrderItemNote> OrderItemNotes { get; set; } = new HashSet<OrderItemNote>();
    }
}
