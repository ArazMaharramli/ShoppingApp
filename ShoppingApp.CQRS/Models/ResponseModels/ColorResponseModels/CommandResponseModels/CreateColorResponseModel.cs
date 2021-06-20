using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels
{
    public class CreateColorResponseModel : BaseResponseModel
    {
        public Color Color { get; set; }
    }

}
