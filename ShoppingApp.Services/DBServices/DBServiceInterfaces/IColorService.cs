using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllActivesAsync();
        Task<IEnumerable<Color>> GetAllHiddenAsync();

        Task<IPagedList<Color>> GetPagedAsync(string searchString, int pageColor, int pageNumber, string sortColumn, string sortDirection, string status);
        Task<IEnumerable<Color>> FindRangeAsync(string[] globalIds);

        Task<Color> FindByTitleAndStatusAsync(string name, Status status = Status.Active);
        Task<Color> FindByGobalIdAsync(string globalId);

        Task<Color> CreateAsync(string title);
        Task<Color> UpdateAsync(Color color);
        Task<List<Color>> UpdateStatusRangeAsync(string[] globalIds, Status status);

        Task<int> DeleteRangeAsync(string[] globalIds);
        Task<int> DeleteAsync(string globalId);
    }
}
