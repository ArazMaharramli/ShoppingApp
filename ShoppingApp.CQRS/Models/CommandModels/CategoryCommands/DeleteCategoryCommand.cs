using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryRangeResponseModel>
    {
        public DeleteCategoryCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
