using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.CommandResponseModels
{
    public class DeleteCategoryRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}
