using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels
{
    public class GetCountryResponseModel : BaseResponseModel
    {
        public Country Country { get; set; }
    }
}
