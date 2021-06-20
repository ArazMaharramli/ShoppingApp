using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels
{
    public class GetPagedColorsResponseModel : BaseResponseModel
    {
        public IPagedList<Color> Colors { get; set; }
    }
}
