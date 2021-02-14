using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.CategoryQueryModels
{
    public class GetCategoriesQuery : IRequest<GetCategoriesResponseModel>
    {

    }
}
