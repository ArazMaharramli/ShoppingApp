using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Repository.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.ProductRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetCategoryTreeAsync(Expression<Func<Category, bool>> predicate);
        Task<IPagedList<Category>> GetPagedCategoriesAsync(Expression<Func<Category, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1);
    }
}
