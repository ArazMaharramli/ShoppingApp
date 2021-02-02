using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.ProductModels
{
    public class SizeType : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }
        public string Abbreviation { get; set; }

        public ICollection<Size> Sizes { get; set; } = new HashSet<Size>();
    }

}
