using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IStoreService
    {
        Task<Store> CreateStoreAsync(
            string storeName, string storeSlug,
            StoreStatus storeStatus, StoreType storeType,
            List<StoreContact> storeContacts, Address storeAddress,
            List<User> users);

        Task<Store> ConfirmStoreAsync(string storeId);
        Task<Store> BlockStoreAsync(string storeId);


        Task<IPagedList<Store>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status);

    }
}
