using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class Category : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }
        public string UniqueSlug { get; set; }
        public string IconUrl { get; set; }

        public int? SortOrder { get; set; }

        public long? ParentId { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; } = new HashSet<Category>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
    }

}
