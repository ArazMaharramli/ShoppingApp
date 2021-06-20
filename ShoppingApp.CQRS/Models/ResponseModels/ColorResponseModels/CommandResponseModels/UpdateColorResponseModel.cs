using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels
{
    public class UpdateColorResponseModel : BaseResponseModel
    {
        public Color Color { get; set; }
    }
}
