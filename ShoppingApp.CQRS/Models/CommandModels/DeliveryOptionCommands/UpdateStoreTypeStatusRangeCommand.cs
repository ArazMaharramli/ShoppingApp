using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands
{
    public class UpdateDeliveryOptionStatusRangeCommand : IRequest<UpdateDeliveryOptionStatusRangeResponseModel>
    {
        public UpdateDeliveryOptionStatusRangeCommand(string[] globalIds, Status status)
        {
            GlobalIds = globalIds;
            Status = status;
        }

        public string[] GlobalIds { get; set; }
        public Status Status { get; set; }
    }
}
