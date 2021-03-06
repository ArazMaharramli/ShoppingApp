using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels
{
    public class GetPagedSizesResponseModel : BaseResponseModel
    {
        public IPagedList<Size> Sizes { get; set; }
    }
}
