using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Classes;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Size> CreateAsync(string title)
        {
            var size = new Size
            {
                UniqueTitle = title,
                Status = Status.Active
            };
            _unitOfWork.Sizes.Add(size);
            await _unitOfWork.SaveChangesAsync();
            return size;
        }

        public Task<int> DeleteAsync(string globalId)
        {
            var storetype = _unitOfWork.Sizes.GetAsync(x => x.GlobalId == globalId);
            if (!(storetype.Result is null))
            {
                storetype.Result.Status = Status.Deleted;
                _unitOfWork.Sizes.Update(storetype.Result);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<int> DeleteRangeAsync(string[] globalIds)
        {
            var sizes = _unitOfWork.Sizes.FindAsync(x => globalIds.Contains(x.GlobalId)).Result.ToList();
            if (!(sizes is null))
            {
                for (int i = 0; i < sizes.Count(); i++)
                {
                    sizes[i].Status = Status.Deleted;
                }
                _unitOfWork.Sizes.UpdateRange(sizes);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<Size> FindByGobalIdAsync(string globalId)
        {
            return _unitOfWork.Sizes.GetAsync(x => x.GlobalId == globalId);
        }


        public Task<Size> FindByTitleAndStatusAsync(string title, Status status = Status.Active)
        {
            return _unitOfWork.Sizes.GetAsync(x => x.UniqueTitle.ToLower() == title.Trim().ToLower());
        }

        public Task<IEnumerable<Size>> FindRangeAsync(string[] globalIds)
        {
            return _unitOfWork.Sizes.FindAsync(x => globalIds.Contains(x.GlobalId));
        }

        public Task<IEnumerable<Size>> GetAllActivesAsync()
        {
            return _unitOfWork.Sizes.FindAsync(x => x.Status == Status.Active);
        }

        public Task<IEnumerable<Size>> GetAllHiddenAsync()
        {
            return _unitOfWork.Sizes.FindAsync(x => x.Status == Status.Hidden);
        }

        public Task<IPagedList<Size>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<Size, bool>> predicate;
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

            return _unitOfWork.Sizes.GetPagedAsync(
                predicate: predicate,
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection);
        }

        public Task<Size> UpdateAsync(Size size)
        {
            size.UpdatedDate = LocalTime.Now();
            _unitOfWork.Sizes.Update(size);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(size);
        }

        public async Task<List<Size>> UpdateStatusRangeAsync(string[] globalIds, Status status)
        {
            var sizes = (await _unitOfWork.Sizes.FindAsync(x => globalIds.Contains(x.GlobalId))).ToList();
            for (int i = 0; i < sizes.Count; i++)
            {
                sizes[i].UpdatedDate = LocalTime.Now();
                sizes[i].Status = status;
            }
            _unitOfWork.Sizes.UpdateRange(sizes);
            await _unitOfWork.SaveChangesAsync();

            return sizes;
        }
    }
}
