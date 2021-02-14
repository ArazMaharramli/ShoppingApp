using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.CategoryQueryModels
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryResponseModel>
    {
        public GetCategoryByIdQuery(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
