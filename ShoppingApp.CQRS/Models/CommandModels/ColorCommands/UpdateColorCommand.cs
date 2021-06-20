using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.ColorCommands
{
    public class UpdateColorCommand : IRequest<UpdateColorResponseModel>
    {
        public UpdateColorCommand(string globalId, string title)
        {
            Title = title;
            GlobalId = globalId;
        }

        public string Title { get; set; }
        public string GlobalId { get; set; }
    }
}
