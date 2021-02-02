using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.OrderModels
{
    public class OrderItemNote : BaseEntitySimple<Status>
    {
        public string Note { get; set; }

        public long OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
