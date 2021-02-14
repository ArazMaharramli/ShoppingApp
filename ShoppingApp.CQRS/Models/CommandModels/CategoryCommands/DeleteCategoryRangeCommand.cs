using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CategoryCommands
{
    public class DeleteCategoryRangeCommand : IRequest<DeleteCategoryRangeResponseModel>
    {
        public DeleteCategoryRangeCommand(string[] globalIds)
        {
            GlobalIds = globalIds;
        }

        public string[] GlobalIds { get; set; }
    }
}
