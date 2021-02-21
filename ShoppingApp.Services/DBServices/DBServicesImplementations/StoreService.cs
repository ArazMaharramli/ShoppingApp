using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;


        public StoreService(IUnitOfWork unitOfWork, ICountryService countryService, IStoreTypeService storeTypeService)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Store> BlockStoreAsync(string storeId)
        {
            var store = await _unitOfWork.Stores.GetAsync(x => x.GlobalId == storeId && x.Status == StoreStatus.PendingConfirmation);
            store.Status = StoreStatus.NotConfirmed;
            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<Store> ConfirmStoreAsync(string storeId)
        {
            var store = await _unitOfWork.Stores.GetAsync(x => x.GlobalId == storeId && x.Status == StoreStatus.PendingConfirmation);
            store.Status = StoreStatus.Active;
            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<Store> CreateStoreAsync(
            string storeName, string storeSlug,
            StoreStatus storeStatus, StoreType storeType,
            List<StoreContact> storeContacts, Address storeAddress,
            List<User> users)
        {
            var store = new Store
            {
                StoreName = storeName,
                UniqueSlug = storeSlug,
                Status = storeStatus,
                StoreContacts = storeContacts,
                StoreType = storeType,
                Users = users,
                Address = storeAddress
            };

            _unitOfWork.Stores.Add(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public Task<IPagedList<Store>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<Store, bool>> predicate;
            switch (status?.ToLower())
            {
                case ("confirmed"):
                    predicate = x => x.Status == StoreStatus.Confirmed;
                    break;
                case ("deleted"):
                    predicate = x => x.Status == StoreStatus.Deleted;
                    break;
                case ("pendingponfirmation"):
                    predicate = x => x.Status == StoreStatus.PendingConfirmation;
                    break;
                case ("notconfirmed"):
                    predicate = x => x.Status == StoreStatus.NotConfirmed;
                    break;
                default:
                    predicate = x => true;
                    break;
            }

            return _unitOfWork.Stores.GetPagedAsync(
                predicate: predicate,
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection);
        }
    }
}
