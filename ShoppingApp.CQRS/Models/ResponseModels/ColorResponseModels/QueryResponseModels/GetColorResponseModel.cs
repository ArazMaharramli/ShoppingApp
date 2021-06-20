
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels
{
    public class GetColorResponseModel : BaseResponseModel
    {
        public Color Color { get; set; }
    }
}
