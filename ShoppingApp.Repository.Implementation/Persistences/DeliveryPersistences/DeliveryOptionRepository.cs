using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Repository.Implementation.Repositories.DeliveryRepositories;
using ShoppingApp.Repository.Persistences;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Persistences.DeliveryPersistences
{
    public class DeliveryOptionRepository : Repository<DeliveryOption>, IDeliveryOptionRepository
    {
        public DeliveryOptionRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;

        public async Task<IPagedList<DeliveryOption>> GetPagedAsync(Expression<Func<DeliveryOption, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1)
        {
            var list = Context.Set<DeliveryOption>()
                .Where(predicate);

            var total = list.Count();
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
            {
                if (sortDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case nameof(DeliveryOption.UniqueName):
                            list = list.OrderBy(p => p.UniqueName);
                            break;
                        case nameof(DeliveryOption.AddedDate):
                            list = list.OrderBy(p => p.AddedDate);
                            break;

                        default:
                            list = list.OrderBy(p => p.AddedDate);
                            break;
                    }
                }
                else if (sortDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case nameof(DeliveryOption.UniqueName):
                            list = list.OrderByDescending(p => p.UniqueName);
                            break;
                        case nameof(DeliveryOption.AddedDate):
                            list = list.OrderByDescending(p => p.AddedDate);
                            break;

                        default:
                            list = list.OrderByDescending(p => p.AddedDate);
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(m =>
                        m.UniqueName.Contains(searchString) ||
                        m.AddedDate.ToString().Contains(searchString)
                    );
            }


            var filtered = list.Count();

            var data = await list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageCount = (total / pageSize) + ((total % pageSize) > 0 ? 1 : 0);
            return new PagedList<DeliveryOption>(
                data: data,
                total: total,
                pageSize: pageSize,
                pageNumber: pageNumber,
                pageCount: pageCount);
        }
    }
}
