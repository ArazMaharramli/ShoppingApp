using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels
{
    public class GetCountriesResponseModel : BaseResponseModel
    {
        public IEnumerable<Country> Countries { get; set; }
    }
}
