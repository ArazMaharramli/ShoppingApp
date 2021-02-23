using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels
{
    public class GetStoreByIdQuery : IRequest<GetStoreByIdResponseModel>
    {
        public GetStoreByIdQuery(string storeId)
        {
            StoreId = storeId;
        }

        public string StoreId { get; set; }
    }
}
