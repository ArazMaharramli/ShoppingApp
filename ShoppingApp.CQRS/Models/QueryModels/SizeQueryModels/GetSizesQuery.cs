using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.SizeQueryModels
{
    public class GetSizesQuery : IRequest<GetSizesResponseModel>
    {

    }
}
