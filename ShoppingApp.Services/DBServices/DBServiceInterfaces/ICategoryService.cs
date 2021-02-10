using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Category>> GetAllHiddenCategoriesAsync();

        Task<IPagedList<Category>> GetPagedCategoriesAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status);
        Task<IEnumerable<Category>> FindRangeAsync(string[] globalIds);

        Task<Category> FindByNameAndStatusAsync(string name, Status status = Status.Active);
        Task<Category> FindByGobalIdAsync(string globalId);
        Task<Category> GetBySlugAsync(string slug);

        Task<Category> CreateAsync(string name, string slug, Category parentCategory);
        Task<Category> UpdateAsync(Category category);
        Task<List<Category>> UpdateStatusRangeAsync(string[] globalIds, Status status);

        Task<int> DeleteRangeAsync(string[] globalIds);
        Task<int> DeleteAsync(string globalId);
    }
}
