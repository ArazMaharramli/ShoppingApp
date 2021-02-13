using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands
{
    public class UpdateStoreTypeCommand : IRequest<UpdateStoreTypeResponseModel>
    {
        public UpdateStoreTypeCommand(string globalId, string name)
        {
            Name = name;
            GlobalId = globalId;
        }

        public string Name { get; set; }
        public string GlobalId { get; set; }
    }
}
