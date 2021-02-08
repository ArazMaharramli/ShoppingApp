using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels
{
    public class GetCategoriesResponseModel : BaseResponseModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
