using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Repository.Implementation.Repositories.AddressRepositories;
using ShoppingApp.Repository.Persistences;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Persistences.AddressPersistences
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;

        public async Task<IPagedList<Country>> GetPagedAsync(Expression<Func<Country, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1)
        {
            var list = Context.Set<Country>()
              .Where(predicate);

            var total = list.Count();
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
            {
                if (sortDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case nameof(Country.Name):
                            list = list.OrderBy(p => p.Name);
                            break;
                        case nameof(Country.AddedDate):
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
                        case nameof(Country.Name):
                            list = list.OrderByDescending(p => p.Name);
                            break;
                        case nameof(Country.AddedDate):
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
            return new PagedList<Country>(
                data: data,
                total: total,
                pageSize: pageSize,
                pageNumber: pageNumber,
                pageCount: pageCount);
        }

        public Task<Country> GetWithAllNavigationsAsync(Expression<Func<Country, bool>> predicate)
        {
            return Context.Set<Country>()
                .Include(x => x.Cities)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }
    }
}
