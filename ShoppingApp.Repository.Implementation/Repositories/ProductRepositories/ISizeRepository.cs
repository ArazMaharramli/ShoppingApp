using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Repository.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.ProductRepositories
{
    public interface ISizeRepository : IRepository<Size>
    {
        Task<IPagedList<Size>> GetPagedAsync(Expression<Func<Size, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1);
    }
}
