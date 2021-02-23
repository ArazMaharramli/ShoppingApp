using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreCommands
{
    public class ConfirmStoreCommand : IRequest<UpdateStoreStatusResponseModel>
    {
        public ConfirmStoreCommand(string storeId)
        {
            StoreId = storeId;
        }

        public string StoreId { get; set; }
    }
}
