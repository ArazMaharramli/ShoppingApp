using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels
{
    public class GetStoreTypeByIdQuery : IRequest<GetStoreTypeResponseModel>
    {
        public GetStoreTypeByIdQuery(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
