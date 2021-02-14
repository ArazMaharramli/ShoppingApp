using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IDeliveryOptionService
    {
        Task<IEnumerable<DeliveryOption>> GetAllAsync();
        Task<IEnumerable<DeliveryOption>> GetAllHiddenAsync();

        Task<IPagedList<DeliveryOption>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status);
        Task<IEnumerable<DeliveryOption>> FindRangeAsync(string[] globalIds);

        Task<DeliveryOption> FindByNameAndStatusAsync(string name, Status status = Status.Active);
        Task<DeliveryOption> FindByGobalIdAsync(string globalId);

        Task<DeliveryOption> CreateAsync(string name, string description);
        Task<DeliveryOption> UpdateAsync(DeliveryOption seliveryOption);
        Task<List<DeliveryOption>> UpdateStatusRangeAsync(string[] globalIds, Status status);

        Task<int> DeleteRangeAsync(string[] globalIds);
        Task<int> DeleteAsync(string globalId);
    }
}
