using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CategoryCommands
{
    public class UpdateCategoryCommand : IRequest<UpdateCategoryResponseModel>
    {
        public UpdateCategoryCommand(string globalId, string name, string slug, string parentId, string[] childrenIds)
        {
            Name = name;
            Slug = slug;
            GlobalId = globalId;
            ParentId = parentId;
            ChildrenIds = childrenIds;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string GlobalId { get; set; }
        public string ParentId { get; set; }
        public string[] ChildrenIds { get; set; }
    }
}
