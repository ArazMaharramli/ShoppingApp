using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands
{
    public class DeleteDeliveryOptionRangeCommand : IRequest<DeleteDeliveryOptionRangeResponseModel>
    {
        public DeleteDeliveryOptionRangeCommand(string[] globalIds)
        {
            GlobalIds = globalIds;
        }

        public string[] GlobalIds { get; set; }
    }
}
