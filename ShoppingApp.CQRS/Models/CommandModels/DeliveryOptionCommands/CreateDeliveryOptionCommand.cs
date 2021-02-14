using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands
{
    public class CreateDeliveryOptionCommand : IRequest<CreateDeliveryOptionResponseModel>
    {
        public CreateDeliveryOptionCommand(string name, string description)
        {
            UniqueName = name;
            Description = description;
        }

        public string UniqueName { get; set; }
        public string Description { get; set; }
    }
}
