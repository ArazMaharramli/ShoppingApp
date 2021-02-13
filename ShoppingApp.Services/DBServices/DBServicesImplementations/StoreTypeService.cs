using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Classes;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class StoreTypeService : IStoreTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<StoreType> CreateAsync(string name)
        {
            var storeType = new StoreType
            {
                Name = name,
                Status = Status.Active
            };
            _unitOfWork.StoreTypes.Add(storeType);
            await _unitOfWork.SaveChangesAsync();
            return storeType;
        }

        public Task<int> DeleteAsync(string globalId)
        {
            var storetype = _unitOfWork.StoreTypes.GetAsync(x => x.GlobalId == globalId);
            if (!(storetype.Result is null))
            {
                storetype.Result.Status = Status.Deleted;
                _unitOfWork.StoreTypes.Update(storetype.Result);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<int> DeleteRangeAsync(string[] globalIds)
        {
            var storeTypes = _unitOfWork.StoreTypes.FindAsync(x => globalIds.Contains(x.GlobalId)).Result.ToList();
            if (!(storeTypes is null))
            {
                for (int i = 0; i < storeTypes.Count(); i++)
                {
                    storeTypes[i].Status = Status.Deleted;
                }
                _unitOfWork.StoreTypes.UpdateRange(storeTypes);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<StoreType> FindByGobalIdAsync(string globalId)
        {
            return _unitOfWork.StoreTypes.GetAsync(x => x.GlobalId == globalId);
        }

        public Task<StoreType> FindByNameAndStatusAsync(string name, Status status = Status.Active)
        {
            return _unitOfWork.StoreTypes.GetAsync(x => x.Name.ToLower() == name.Trim().ToLower());
        }

        public Task<IEnumerable<StoreType>> FindRangeAsync(string[] globalIds)
        {
            return _unitOfWork.StoreTypes.FindAsync(x => globalIds.Contains(x.GlobalId));
        }

        public Task<IEnumerable<StoreType>> GetAllAsync()
        {
            return _unitOfWork.StoreTypes.GetAllAsync();
        }

        public Task<IEnumerable<StoreType>> GetAllHiddenAsync()
        {
            return _unitOfWork.StoreTypes.FindAsync(x => x.Status == Status.Hidden);
        }

        public Task<IPagedList<StoreType>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<StoreType, bool>> predicate;
            switch (status?.ToLower())
            {
                case ("active"):
                    predicate = x => x.Status == Status.Active;
                    break;
                case ("deleted"):
                    predicate = x => x.Status == Status.Deleted;
                    break;
                case ("hidden"):
                    predicate = x => x.Status == Status.Hidden;
                    break;
                default:
                    predicate = x => true;
                    break;
            }

            return _unitOfWork.StoreTypes.GetPagedAsync(
                predicate: predicate,
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection);
        }

        public Task<StoreType> UpdateAsync(StoreType storeType)
        {
            storeType.UpdatedDate = LocalTime.Now();
            _unitOfWork.StoreTypes.Update(storeType);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(storeType);
        }

        public Task<List<StoreType>> UpdateStatusRangeAsync(string[] globalIds, Status status)
        {
            var storeTypes = _unitOfWork.StoreTypes.FindAsync(x => globalIds.Contains(x.GlobalId)).Result.ToList();
            for (int i = 0; i < storeTypes.Count; i++)
            {
                storeTypes[i].UpdatedDate = LocalTime.Now();
                storeTypes[i].Status = status;
            }
            _unitOfWork.StoreTypes.UpdateRange(storeTypes);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(storeTypes);
        }
    }
}
