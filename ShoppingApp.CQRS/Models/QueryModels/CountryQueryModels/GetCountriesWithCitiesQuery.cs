using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels
{
    public class GetCountriesWithCitiesQuery : IRequest<GetCountriesResponseModel>
    {

    }
}
