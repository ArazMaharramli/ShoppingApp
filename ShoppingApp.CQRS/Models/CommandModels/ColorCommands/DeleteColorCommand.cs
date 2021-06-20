using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.ColorCommands
{
    public class DeleteColorCommand : IRequest<DeleteColorRangeResponseModel>
    {
        public DeleteColorCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
