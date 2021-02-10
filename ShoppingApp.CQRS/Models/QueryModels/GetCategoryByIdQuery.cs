using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels
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
