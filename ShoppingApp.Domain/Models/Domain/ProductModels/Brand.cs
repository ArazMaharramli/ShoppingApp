using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class Brand : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }
        public string LogoUrl { get; set; }
        public string UniqueSlug { get; set; }


        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }

}
