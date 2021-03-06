using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.SizeCommands
{
    public class DeleteSizeCommand : IRequest<DeleteSizeRangeResponseModel>
    {
        public DeleteSizeCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
