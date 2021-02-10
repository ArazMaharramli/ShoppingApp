using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<CreateCategoryResponseModel>
    {
        public CreateCategoryCommand(string categoryName, string slug, string parentId)
        {
            CategoryName = categoryName;
            Slug = slug;
            ParentId = parentId;
        }

        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string ParentId { get; set; }
    }
}
