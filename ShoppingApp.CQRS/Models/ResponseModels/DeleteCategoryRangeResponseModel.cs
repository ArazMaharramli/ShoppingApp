using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class DeleteCategoryRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}
