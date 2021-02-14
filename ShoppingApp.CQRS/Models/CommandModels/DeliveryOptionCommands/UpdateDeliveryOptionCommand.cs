using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands
{
    public class UpdateDeliveryOptionCommand : IRequest<UpdateDeliveryOptionResponseModel>
    {
        public UpdateDeliveryOptionCommand(string globalId, string uniqueName, string description)
        {
            UniqueName = uniqueName;
            GlobalId = globalId;
            Description = description;
        }

        public string UniqueName { get; set; }
        public string GlobalId { get; set; }

        public string Description { get; set; }
    }
}
