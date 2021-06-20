using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels
{
    public class GetColorByIdQuery : IRequest<GetColorResponseModel>
    {
        public GetColorByIdQuery(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
