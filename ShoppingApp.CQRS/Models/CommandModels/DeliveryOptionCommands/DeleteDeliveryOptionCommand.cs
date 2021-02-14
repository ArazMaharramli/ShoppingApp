using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands
{
    public class DeleteDeliveryOptionCommand : IRequest<DeleteDeliveryOptionRangeResponseModel>
    {
        public DeleteDeliveryOptionCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
