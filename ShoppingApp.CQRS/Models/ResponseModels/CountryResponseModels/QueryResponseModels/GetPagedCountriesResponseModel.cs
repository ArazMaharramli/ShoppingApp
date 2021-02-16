using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels
{
    public class GetPagedCountriesResponseModel : BaseResponseModel
    {
        public IPagedList<Country> Countries { get; set; }
    }
}
