using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels
{
    public class DeleteCountryRangeResponseModel : BaseResponseModel
    {
        public int DeletedCount { get; set; }
    }
}
