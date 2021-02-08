using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels
{
    public class GetCategoriesQuery : IRequest<GetCategoriesResponseModel>
    {

    }
}
