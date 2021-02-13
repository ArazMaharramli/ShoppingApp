using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IStoreTypeService
    {
        Task<IEnumerable<StoreType>> GetAllAsync();
        Task<IEnumerable<StoreType>> GetAllHiddenAsync();

        Task<IPagedList<StoreType>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status);
        Task<IEnumerable<StoreType>> FindRangeAsync(string[] globalIds);

        Task<StoreType> FindByNameAndStatusAsync(string name, Status status = Status.Active);
        Task<StoreType> FindByGobalIdAsync(string globalId);

        Task<StoreType> CreateAsync(string name);
        Task<StoreType> UpdateAsync(StoreType category);
        Task<List<StoreType>> UpdateStatusRangeAsync(string[] globalIds, Status status);

        Task<int> DeleteRangeAsync(string[] globalIds);
        Task<int> DeleteAsync(string globalId);
    }
}
