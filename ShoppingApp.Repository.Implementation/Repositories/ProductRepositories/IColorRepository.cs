using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Repository.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.ProductRepositories
{
    public interface IColorRepository : IRepository<Color>
    {
        Task<IPagedList<Color>> GetPagedAsync(Expression<Func<Color, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageColor = 10, int pageNumber = 1);

    }
}
