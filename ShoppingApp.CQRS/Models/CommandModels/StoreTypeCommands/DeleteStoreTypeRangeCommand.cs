using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands
{
    public class DeleteStoreTypeRangeCommand : IRequest<DeleteStoreTypeRangeResponseModel>
    {
        public DeleteStoreTypeRangeCommand(string[] globalIds)
        {
            GlobalIds = globalIds;
        }

        public string[] GlobalIds { get; set; }
    }
}
