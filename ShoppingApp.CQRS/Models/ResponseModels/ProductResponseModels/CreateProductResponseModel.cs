using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ProductResponseModels
{
    public class CreateProductResponseModel : BaseResponseModel
    {
        public Product Product { get; set; }
    }
}
