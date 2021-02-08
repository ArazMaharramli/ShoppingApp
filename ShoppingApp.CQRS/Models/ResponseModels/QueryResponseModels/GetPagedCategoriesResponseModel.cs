using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels
{
    public class GetPagedCategoriesResponseModel : BaseResponseModel
    {
        public IPagedList<Category> Categories { get; set; }
    }
}
