using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.SizeCommands
{
    public class UpdateSizeCommand : IRequest<UpdateSizeResponseModel>
    {
        public UpdateSizeCommand(string globalId, string title)
        {
            Title = title;
            GlobalId = globalId;
        }

        public string Title { get; set; }
        public string GlobalId { get; set; }
    }
}
