using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ShoppingCartModels
{
    public class ShoppingCartItem : BaseEntitySimple<ShoppingCardItemStatus>
    {
        public int Quantity { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
