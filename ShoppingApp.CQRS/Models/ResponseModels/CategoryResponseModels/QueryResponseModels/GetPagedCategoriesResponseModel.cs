using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.QueryResponseModels
{
    public class GetPagedCategoriesResponseModel : BaseResponseModel
    {
        public IPagedList<Category> Categories { get; set; }
    }
}
