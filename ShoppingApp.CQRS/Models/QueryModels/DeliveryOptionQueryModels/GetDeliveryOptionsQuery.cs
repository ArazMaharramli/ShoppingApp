using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.DeliveryOptionQueryModels
{
    public class GetDeliveryOptionsQuery : IRequest<GetDeliveryOptionsResponseModel>
    {

    }
}
