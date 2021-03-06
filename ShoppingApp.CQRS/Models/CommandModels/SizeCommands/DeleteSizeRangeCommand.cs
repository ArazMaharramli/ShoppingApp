using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.SizeCommands
{
    public class DeleteSizeRangeCommand : IRequest<DeleteSizeRangeResponseModel>
    {
        public DeleteSizeRangeCommand(string[] globalIds)
        {
            GlobalIds = globalIds;
        }

        public string[] GlobalIds { get; set; }
    }
}
