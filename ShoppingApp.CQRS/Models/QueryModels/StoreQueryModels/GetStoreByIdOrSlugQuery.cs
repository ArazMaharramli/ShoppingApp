using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels
{
    public class GetStoreByIdOrSlugQuery : IRequest<GetStoreResponseModel>
    {
        public GetStoreByIdOrSlugQuery(string storeIdOrSlug)
        {
            StoreIdOrSlug = storeIdOrSlug;
        }

        public string StoreIdOrSlug { get; set; }
    }
}
