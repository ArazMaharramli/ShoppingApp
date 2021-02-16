
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels
{
    public class UpdateCountryResponseModel : BaseResponseModel
    {
        public Country Country { get; set; }
    }
}
