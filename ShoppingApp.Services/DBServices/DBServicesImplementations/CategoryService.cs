using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> CreateAsync(string name, string slug, Category parentCategory)
        {

            var category = new Category
            {

                GlobalId = Guid.NewGuid().ToString("N"),
                UniqueName = name,
                UniqueSlug = slug,
                Parent = parentCategory
            };
            _unitOfWork.Categories.Add(category);
            await _unitOfWork.SaveChangesAsync();
            return category;
        }

        public async Task<int> DeleteAsync(string globalId)
        {
            var categoryInDb = await _unitOfWork.Categories.GetAsync(x => x.GlobalId == globalId);
            if (!(categoryInDb is null))
            {
                categoryInDb.Status = Status.Deleted;
                _unitOfWork.Categories.Update(categoryInDb);

                return await _unitOfWork.SaveChangesAsync(); ;
            }
            return 0;
        }

        public async Task<int> DeleteRangeAsync(string[] globalIds)
        {
            var categoriesInDb = (await _unitOfWork.Categories.FindAsync(x => globalIds.Contains(x.GlobalId)))?.ToList();
            if (!(categoriesInDb is null))
            {
                for (int i = 0; i < categoriesInDb.Count(); i++)
                {
                    categoriesInDb[i].Status = Status.Deleted;
                }

                _unitOfWork.Categories.UpdateRange(categoriesInDb);

                return await _unitOfWork.SaveChangesAsync(); ;
            }
            return 0;
        }

        public Task<Category> FindByGobalIdAsync(string globalId)
        {
            return _unitOfWork.Categories.GetAsync(x => x.GlobalId == globalId);
        }

        public Task<Category> FindByNameAndStatusAsync(string name, Status status = Status.Active)
        {
            return _unitOfWork.Categories.GetAsync(x => x.UniqueName.Trim().ToLower() == name.Trim().ToLower() && x.Status == status);
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _unitOfWork.Categories.GetCategoryTreeAsync(x => x.Status != Status.Deleted);
        }

        public Task<Category> GetBySlugAsync(string slug)
        {
            return _unitOfWork.Categories.GetAsync(x => x.UniqueSlug.ToLower() == slug.ToLower());
        }

        public Task<IPagedList<Category>> GetPagedCategoriesAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<Category, bool>> predicate;
            switch (status?.ToLower())
            {
                case ("active"):
                    predicate = x => x.Status == Status.Active;
                    break;
                case ("deleted"):
                    predicate = x => x.Status == Status.Deleted;
                    break;
                case ("hidden"):
                    predicate = x => x.Status == Status.Hidden;
                    break;
                default:
                    predicate = x => true;
                    break;
            }

            return _unitOfWork.Categories.GetPagedCategoriesAsync(
               predicate: predicate,
               searchString: searchString,
               pageSize: pageSize,
               pageNumber: pageNumber,
               sortColumn: sortColumn,
               sortDirection: sortDirection);
        }

        public async Task<List<Category>> UpdateStatusRangeAsync(string[] globalIds, Status status)
        {
            var categoriesInDb = (await _unitOfWork.Categories.FindAsync(x => globalIds.Contains(x.GlobalId)))?.ToList();
            if (!(categoriesInDb is null))
            {
                for (int i = 0; i < categoriesInDb.Count(); i++)
                {
                    categoriesInDb[i].Status = status;
                }

                _unitOfWork.Categories.UpdateRange(categoriesInDb);
                await _unitOfWork.SaveChangesAsync();
                return categoriesInDb;
            }
            return null;
        }
    }
}
