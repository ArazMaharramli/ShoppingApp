using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.QueryResponseModels
{
    public class GetCategoriesResponseModel : BaseResponseModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
