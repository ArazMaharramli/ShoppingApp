using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class Size : BaseEntitySimple<Status>
    {
        public string UniqueTitle { get; set; }

        public long SizeTypeId { get; set; }
        public SizeType SizeType { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; } = new HashSet<ProductDetail>();
    }

}
