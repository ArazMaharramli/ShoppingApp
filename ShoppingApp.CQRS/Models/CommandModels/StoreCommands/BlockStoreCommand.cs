using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreCommands
{
    public class BlockStoreCommand : IRequest<UpdateStoreStatusResponseModel>
    {
        public BlockStoreCommand(string storeId)
        {
            StoreId = storeId;
        }

        public string StoreId { get; set; }
    }
}
