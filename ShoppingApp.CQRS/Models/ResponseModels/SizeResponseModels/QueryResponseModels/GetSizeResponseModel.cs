
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels
{
    public class GetSizeResponseModel : BaseResponseModel
    {
        public Size Size { get; set; }
    }
}
