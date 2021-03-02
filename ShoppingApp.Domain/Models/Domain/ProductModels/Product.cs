using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Domain.Models.Domain.MediaModels;
using ShoppingApp.Domain.Models.Domain.ShoppingCartModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{

    public class Product : BaseEntitySimple<ProductStatus>
    {
        public string Title { get; set; }
        public string UniqueSlug { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }

        public long BrandId { get; set; }
        public Brand Brand { get; set; }

        public long MaterialId { get; set; }
        public Material Material { get; set; }

        public long StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; } = new HashSet<ProductDetail>();
        public ICollection<ProductMedia> ProductMedias { get; set; } = new HashSet<ProductMedia>();
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
        public ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();


    }

}
