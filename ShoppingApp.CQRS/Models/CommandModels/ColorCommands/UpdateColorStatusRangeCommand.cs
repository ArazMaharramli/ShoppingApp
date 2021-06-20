using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.ColorCommands
{
    public class UpdateColorStatusRangeCommand : IRequest<UpdateColorStatusRangeResponseModel>
    {
        public UpdateColorStatusRangeCommand(string[] globalIds, Status status)
        {
            GlobalIds = globalIds;
            Status = status;
        }

        public string[] GlobalIds { get; set; }
        public Status Status { get; set; }
    }
}
