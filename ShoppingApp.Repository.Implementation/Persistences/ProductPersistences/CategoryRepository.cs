using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Repository.Implementation.Repositories.ProductRepositories;
using ShoppingApp.Repository.Persistences;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Persistences.ProductPersistences
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;

        public Task<List<Category>> GetCategoryTreeAsync(Expression<Func<Category, bool>> predicate)
        {
            return Context.Set<Category>()
                .Include(x => x.Children)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IPagedList<Category>> GetPagedCategoriesAsync(Expression<Func<Category, bool>> predicate, string searchString, string sortColumn, string sortDirection, int pageSize = 10, int pageNumber = 1)
        {
            var categories = Context.Set<Category>()
               .Include(x => x.Children)
               .Where(predicate);

            var total = categories.Count();
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
            {
                if (sortDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case nameof(Category.UniqueName):
                            categories = categories.OrderBy(p => p.UniqueName);
                            break;
                        case nameof(Category.AddedDate):
                            categories = categories.OrderBy(p => p.AddedDate);
                            break;

                        default:
                            categories = categories.OrderBy(p => p.AddedDate);
                            break;
                    }
                }
                else if (sortDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case nameof(Category.UniqueName):
                            categories = categories.OrderByDescending(p => p.UniqueName);
                            break;
                        case nameof(Category.AddedDate):
                            categories = categories.OrderByDescending(p => p.AddedDate);
                            break;

                        default:
                            categories = categories.OrderByDescending(p => p.AddedDate);
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(m =>
                        m.UniqueName.Contains(searchString) ||
                        m.AddedDate.ToString().Contains(searchString)
                    );
            }


            var filtered = categories.Count();

            var data = await categories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageCount = (total / pageSize) + ((total % pageSize) > 0 ? 1 : 0);
            return new PagedList<Category>(
                data: data,
                total: total,
                pageSize: pageSize,
                pageNumber: pageNumber,
                pageCount: pageCount);
        }

        public Task<Category> GetWithAllNavigationsAsync(Expression<Func<Category, bool>> predicate)
        {
            return Context.Set<Category>()
                .Include(x => x.Children)
                .Include(x => x.Parent)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }
    }
}
