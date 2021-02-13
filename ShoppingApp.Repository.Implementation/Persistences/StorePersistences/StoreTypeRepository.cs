using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Repository.Implementation.Repositories.StoreRepositories;
using ShoppingApp.Repository.Persistences;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Persistences.StorePersistences
{
    public class StoreTypeRepository : Repository<StoreType>, IStoreTypeRepository
    {
        public StoreTypeRepository(ShoppingAppDbContext context) : base(context)
        {

        }
        public async Task<IPagedList<StoreType>> GetPagedAsync(
            Expression<Func<StoreType, bool>> predicate,
            string searchString,
            string sortColumn,
            string sortDirection,
            int pageSize = 10,
            int pageNumber = 1)
        {
            var list = Context.Set<StoreType>()
               .Where(predicate);

            var total = list.Count();
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
            {
                if (sortDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case nameof(StoreType.Name):
                            list = list.OrderBy(p => p.Name);
                            break;
                        case nameof(StoreType.AddedDate):
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
                        case nameof(StoreType.Name):
                            list = list.OrderByDescending(p => p.Name);
                            break;
                        case nameof(StoreType.AddedDate):
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
                        m.Name.Contains(searchString) ||
                        m.AddedDate.ToString().Contains(searchString)
                    );
            }


            var filtered = list.Count();

            var data = await list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageCount = (total / pageSize) + ((total % pageSize) > 0 ? 1 : 0);
            return new PagedList<StoreType>(
                data: data,
                total: total,
                pageSize: pageSize,
                pageNumber: pageNumber,
                pageCount: pageCount);
        }
        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
