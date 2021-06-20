using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MediaModels
{
    public class ProductMedia : BaseEntitySimple<Status>
    {
        public string Url { get; set; }
        public string AltAttribute { get; set; }

        public MediaType MediaType { get; set; }
        public byte OrderIndex { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
