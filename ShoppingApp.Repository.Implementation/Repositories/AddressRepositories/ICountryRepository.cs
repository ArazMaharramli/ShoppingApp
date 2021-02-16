using ShoppingApp.Repository.Repositories;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.AddressRepositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetWithAllNavigationsAsync(Expression<Func<Country, bool>> predicate);
        Task<IPagedList<Country>> GetPagedAsync(Expression<Func<Country, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1);
    }
}
