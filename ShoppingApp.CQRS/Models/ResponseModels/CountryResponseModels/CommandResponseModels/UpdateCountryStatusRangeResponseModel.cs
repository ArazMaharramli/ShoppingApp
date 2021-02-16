using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels
{
    public class UpdateCountryStatusRangeResponseModel : BaseResponseModel
    {
        public List<Country> Countries { get; set; }
    }
}
