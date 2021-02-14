using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.CommandResponseModels
{
    public class UpdateCategoryResponseModel : BaseResponseModel
    {
        public Category Category { get; set; }
    }
}
