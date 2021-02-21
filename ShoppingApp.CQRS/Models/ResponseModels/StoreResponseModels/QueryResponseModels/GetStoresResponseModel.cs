using System;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.QueryResponseModels
{
    public class GetPagedStoresResponseModel : BaseResponseModel
    {
        public IPagedList<Store> Stores { get; set; }
    }
}
