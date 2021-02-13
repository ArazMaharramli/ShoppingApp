using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands
{
    public class DeleteStoreTypeCommand : IRequest<DeleteStoreTypeRangeResponseModel>
    {
        public DeleteStoreTypeCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
