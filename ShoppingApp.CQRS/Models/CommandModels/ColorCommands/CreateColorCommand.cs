using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.ColorCommands
{
    public class CreateColorCommand : IRequest<CreateColorResponseModel>
    {
        public CreateColorCommand(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
