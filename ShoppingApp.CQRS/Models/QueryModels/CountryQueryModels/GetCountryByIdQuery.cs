using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels
{
    public class GetCountryByIdQuery : IRequest<GetCountryResponseModel>
    {
        public GetCountryByIdQuery(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
