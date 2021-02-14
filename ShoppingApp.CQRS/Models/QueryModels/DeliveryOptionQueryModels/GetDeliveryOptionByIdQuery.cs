using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.DeliveryOptionQueryModels
{
    public class GetDeliveryOptionByIdQuery : IRequest<GetDeliveryOptionResponseModel>
    {
        public GetDeliveryOptionByIdQuery(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
