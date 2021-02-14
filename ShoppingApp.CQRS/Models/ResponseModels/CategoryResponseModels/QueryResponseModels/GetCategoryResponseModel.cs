using System;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.QueryResponseModels
{
    public class GetCategoryResponseModel : BaseResponseModel
    {
        public Category Category { get; set; }
    }
}
