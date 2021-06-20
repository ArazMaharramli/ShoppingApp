using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels
{
    public class GetColorsQuery : IRequest<GetColorsResponseModel>
    {

    }
}
