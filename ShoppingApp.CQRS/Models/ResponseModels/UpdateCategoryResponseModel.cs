using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class UpdateCategoryResponseModel : BaseResponseModel
    {
        public Category Category { get; set; }
    }
}
