using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Repository.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.StoreRepositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<IPagedList<Store>> GetPagedAsync(Expression<Func<Store, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1);
        Task<Store> GetWithAllNavigationsAsync(Expression<Func<Store, bool>> predicate);
        Task<Store> GetWithOwnerAsync(Expression<Func<Store, bool>> predicate);
    }
}
