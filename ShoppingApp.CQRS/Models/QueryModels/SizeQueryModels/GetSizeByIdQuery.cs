using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.SizeQueryModels
{
    public class GetSizeByIdQuery : IRequest<GetSizeResponseModel>
    {
        public GetSizeByIdQuery(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
