using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels
{
    public class GetStoreByOwnerIdQuery : IRequest<GetStoreResponseModel>
    {
        public GetStoreByOwnerIdQuery(string ownerId)
        {
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }
    }
}
