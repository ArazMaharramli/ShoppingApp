using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class Tag : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
    }

}
