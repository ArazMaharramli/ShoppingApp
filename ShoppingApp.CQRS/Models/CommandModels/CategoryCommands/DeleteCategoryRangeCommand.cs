using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

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
    public class DeleteCategoryCommand : IRequest<DeleteCategoryRangeResponseModel>
    {
        public DeleteCategoryCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
