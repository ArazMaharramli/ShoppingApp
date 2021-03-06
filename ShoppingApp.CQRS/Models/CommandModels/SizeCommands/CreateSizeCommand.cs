using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.SizeCommands
{
    public class CreateSizeCommand : IRequest<CreateSizeResponseModel>
    {
        public CreateSizeCommand(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
