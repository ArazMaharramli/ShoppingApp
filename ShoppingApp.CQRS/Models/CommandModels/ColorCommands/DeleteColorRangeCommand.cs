using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.ColorCommands
{
    public class DeleteColorRangeCommand : IRequest<DeleteColorRangeResponseModel>
    {
        public DeleteColorRangeCommand(string[] globalIds)
        {
            GlobalIds = globalIds;
        }

        public string[] GlobalIds { get; set; }
    }
}
