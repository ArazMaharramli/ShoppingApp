using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels
{
    public class GetStoreTypesQuery : IRequest<GetStoreTypesResponseModel>
    {

    }
}
