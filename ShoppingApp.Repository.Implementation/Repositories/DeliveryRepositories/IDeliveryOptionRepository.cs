using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Repository.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Repositories.DeliveryRepositories
{
    public interface IDeliveryOptionRepository : IRepository<DeliveryOption>
    {
        Task<IPagedList<DeliveryOption>> GetPagedAsync(Expression<Func<DeliveryOption, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1);
    }
}
