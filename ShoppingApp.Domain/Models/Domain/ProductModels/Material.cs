using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class Material : BaseEntitySimple<Status>
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }

}
