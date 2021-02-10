using System;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels
{
    public class GetCategoryResponseModel : BaseResponseModel
    {
        public Category Category { get; set; }
    }
}
