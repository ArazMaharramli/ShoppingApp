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
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Color> CreateAsync(string title)
        {
            var color = new Color
            {
                UniqueTitle = title,
                Status = Status.Active
            };
            _unitOfWork.Colors.Add(color);
            await _unitOfWork.SaveChangesAsync();
            return color;
        }

        public Task<int> DeleteAsync(string globalId)
        {
            var storetype = _unitOfWork.Colors.GetAsync(x => x.GlobalId == globalId);
            if (!(storetype.Result is null))
            {
                storetype.Result.Status = Status.Deleted;
                _unitOfWork.Colors.Update(storetype.Result);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<int> DeleteRangeAsync(string[] globalIds)
        {
            var colors = _unitOfWork.Colors.FindAsync(x => globalIds.Contains(x.GlobalId)).Result.ToList();
            if (!(colors is null))
            {
                for (int i = 0; i < colors.Count(); i++)
                {
                    colors[i].Status = Status.Deleted;
                }
                _unitOfWork.Colors.UpdateRange(colors);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<Color> FindByGobalIdAsync(string globalId)
        {
            return _unitOfWork.Colors.GetAsync(x => x.GlobalId == globalId);
        }

        public Task<Color> FindByTitleAndStatusAsync(string title, Status status = Status.Active)
        {
            return _unitOfWork.Colors.GetAsync(x => x.UniqueTitle.ToLower() == title.Trim().ToLower());
        }

        public Task<IEnumerable<Color>> FindRangeAsync(string[] globalIds)
        {
            return _unitOfWork.Colors.FindAsync(x => globalIds.Contains(x.GlobalId));
        }

        public Task<IEnumerable<Color>> GetAllActivesAsync()
        {
            return _unitOfWork.Colors.FindAsync(x => x.Status == Status.Active);
        }

        public Task<IEnumerable<Color>> GetAllHiddenAsync()
        {
            return _unitOfWork.Colors.FindAsync(x => x.Status == Status.Hidden);
        }

        public Task<IPagedList<Color>> GetPagedAsync(string searchString, int pageColor, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<Color, bool>> predicate;
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

            return _unitOfWork.Colors.GetPagedAsync(
                predicate: predicate,
                searchString: searchString,
                pageColor: pageColor,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection);
        }

        public Task<Color> UpdateAsync(Color color)
        {
            color.UpdatedDate = LocalTime.Now();
            _unitOfWork.Colors.Update(color);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(color);
        }

        public async Task<List<Color>> UpdateStatusRangeAsync(string[] globalIds, Status status)
        {
            var colors = (await _unitOfWork.Colors.FindAsync(x => globalIds.Contains(x.GlobalId))).ToList();
            for (int i = 0; i < colors.Count; i++)
            {
                colors[i].UpdatedDate = LocalTime.Now();
                colors[i].Status = status;
            }
            _unitOfWork.Colors.UpdateRange(colors);
            await _unitOfWork.SaveChangesAsync();

            return colors;
        }
    }
}
