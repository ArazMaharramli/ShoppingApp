using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands
{
    public class UpdateStoreTypeStatusRangeCommand : IRequest<UpdateStoreTypeStatusRangeResponseModel>
    {
        public UpdateStoreTypeStatusRangeCommand(string[] globalIds, Status status)
        {
            GlobalIds = globalIds;
            Status = status;
        }

        public string[] GlobalIds { get; set; }
        public Status Status { get; set; }
    }
}
