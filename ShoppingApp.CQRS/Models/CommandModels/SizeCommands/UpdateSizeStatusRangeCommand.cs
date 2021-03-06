using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.SizeCommands
{
    public class UpdateSizeStatusRangeCommand : IRequest<UpdateSizeStatusRangeResponseModel>
    {
        public UpdateSizeStatusRangeCommand(string[] globalIds, Status status)
        {
            GlobalIds = globalIds;
            Status = status;
        }

        public string[] GlobalIds { get; set; }
        public Status Status { get; set; }
    }
}
