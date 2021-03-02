using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.OrderModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class ProductDetail : BaseEntitySimple<Status>
    {
        public int StockQuantity { get; set; }

        public double Price { get; set; }
        public double DiscountedPrice { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long ColorId { get; set; }
        public Color Color { get; set; }

        public long SizeId { get; set; }
        public Size Size { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

}
