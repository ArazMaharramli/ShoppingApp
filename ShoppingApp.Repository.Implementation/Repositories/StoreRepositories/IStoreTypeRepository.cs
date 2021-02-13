using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Repository.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.StoreRepositories
{
    public interface IStoreTypeRepository : IRepository<StoreType>
    {
        Task<IPagedList<StoreType>> GetPagedAsync(Expression<Func<StoreType, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1);

    }
}
