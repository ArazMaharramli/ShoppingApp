using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels
{
    public class UpdateSizeResponseModel : BaseResponseModel
    {
        public Size Size { get; set; }
    }
}
