using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands
{
    public class CreateStoreTypeCommand : IRequest<CreateStoreTypeResponseModel>
    {
        public CreateStoreTypeCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
