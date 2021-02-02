using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MappingModels
{
    public class ProductCategory : BaseEntity<Status>
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

