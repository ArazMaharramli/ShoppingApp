﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Classes;
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
            var store = await _unitOfWork.Stores.GetWithAllNavigationsAsync(x => x.GlobalId == storeId && x.Status != StoreStatus.Deleted);
            if (store is null)
            {
                return store;
            }
            store.Status = StoreStatus.NotConfirmed;
            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<Store> ConfirmStoreAsync(string storeId)
        {
            var store = await _unitOfWork.Stores.GetWithAllNavigationsAsync(x => x.GlobalId == storeId && x.Status != StoreStatus.Deleted);
            if (store is null)
            {
                return store;
            }
            store.Status = StoreStatus.Confirmed;
            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<Store> CreateStoreAsync(
            string storeName, string storeSlug,
            StoreStatus storeStatus, StoreType storeType,
            List<StoreContact> storeContacts, Address storeAddress,
            User owner)
        {
            var store = new Store
            {
                StoreName = storeName,
                UniqueSlug = storeSlug,
                Status = storeStatus,
                StoreContacts = storeContacts,
                StoreType = storeType,
                Owner = owner,
                Address = storeAddress
            };

            _unitOfWork.Stores.Add(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public Task<Store> FindByIdOrSlugAsync(string storeIdOrSlug)
        {
            return _unitOfWork.Stores.GetWithAllNavigationsAsync(x => x.GlobalId == storeIdOrSlug || x.UniqueSlug == storeIdOrSlug);
        }

        public Task<Store> FindByOwnerIdAsync(string ownerId)
        {
            return _unitOfWork.Stores.GetWithAllNavigationsAsync(x => x.Owner.Id == ownerId);
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
                case ("pendingconfirmation"):
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

        public async Task<bool> IsSlugAvailable(string slug)
        {
            var store = await _unitOfWork.Stores.GetAsync(x => x.UniqueSlug == slug);
            return store == null;
        }

        public async Task<Store> UpdateProfilePhotoAsync(string userId, string photoUrl)
        {
            var store = await _unitOfWork.Stores.GetWithOwnerAsync(x => x.Owner.Id == userId);
            if (store is null)
            {
                throw new Exception("Store Not Found");
            }
            store.ProfilePhotoUrl = photoUrl;
            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateStoreAsync(Store store)
        {
            store.UpdatedDate = LocalTime.Now();
            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }
    }
}
