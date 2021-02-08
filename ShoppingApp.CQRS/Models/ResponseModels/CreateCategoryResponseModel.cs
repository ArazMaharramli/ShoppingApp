using System;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class CreateCategoryResponseModel : BaseResponseModel
    {
        public Category Category { get; set; }
    }
}
