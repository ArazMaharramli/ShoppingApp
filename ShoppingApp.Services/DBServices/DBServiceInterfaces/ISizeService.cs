using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface ISizeService
    {
        Task<IEnumerable<Size>> GetAllActivesAsync();
        Task<IEnumerable<Size>> GetAllHiddenAsync();

        Task<IPagedList<Size>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status);
        Task<IEnumerable<Size>> FindRangeAsync(string[] globalIds);

        Task<Size> FindByTitleAndStatusAsync(string title, Status status = Status.Active);
        Task<Size> FindByGobalIdAsync(string globalId);

        Task<Size> CreateAsync(string title);
        Task<Size> UpdateAsync(Size size);
        Task<List<Size>> UpdateStatusRangeAsync(string[] globalIds, Status status);

        Task<int> DeleteRangeAsync(string[] globalIds);
        Task<int> DeleteAsync(string globalId);
    }
}
