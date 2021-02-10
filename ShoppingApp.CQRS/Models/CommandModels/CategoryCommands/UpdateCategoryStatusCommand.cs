using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.CategoryCommands
{
    public class UpdateCategoryStatusRangeCommand : IRequest<UpdateCategoryStatusRangeResponseModel>
    {
        public UpdateCategoryStatusRangeCommand(string[] globalIds, Status status)
        {
            GlobalIds = globalIds;
            Status = status;
        }

        public string[] GlobalIds { get; set; }
        public Status Status { get; set; }
    }
}
