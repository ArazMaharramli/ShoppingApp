using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Classes;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class DeliveryOptionService : IDeliveryOptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryOptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DeliveryOption> CreateAsync(string name, string description)
        {
            var deliveryOption = new DeliveryOption
            {
                UniqueName = name.Trim(),
                Description = description.Trim(),
                Status = Status.Active
            };
            _unitOfWork.DeliveryOptions.Add(deliveryOption);
            await _unitOfWork.SaveChangesAsync();
            return deliveryOption;
        }

        public Task<int> DeleteAsync(string globalId)
        {
            var deliveryOption = _unitOfWork.DeliveryOptions.GetAsync(x => x.GlobalId == globalId);
            if (!(deliveryOption.Result is null))
            {
                deliveryOption.Result.Status = Status.Deleted;
                _unitOfWork.DeliveryOptions.Update(deliveryOption.Result);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<int> DeleteRangeAsync(string[] globalIds)
        {
            var deliveryOptions = _unitOfWork.DeliveryOptions.FindAsync(x => globalIds.Contains(x.GlobalId)).Result.ToList();
            if (!(deliveryOptions is null))
            {
                for (int i = 0; i < deliveryOptions.Count(); i++)
                {
                    deliveryOptions[i].Status = Status.Deleted;
                }
                _unitOfWork.DeliveryOptions.UpdateRange(deliveryOptions);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task<DeliveryOption> FindByGobalIdAsync(string globalId)
        {
            return _unitOfWork.DeliveryOptions.GetAsync(x => x.GlobalId == globalId);
        }

        public Task<DeliveryOption> FindByNameAndStatusAsync(string name, Status status = Status.Active)
        {
            return _unitOfWork.DeliveryOptions.GetAsync(x => x.UniqueName.ToLower() == name.Trim().ToLower());
        }

        public Task<IEnumerable<DeliveryOption>> FindRangeAsync(string[] globalIds)
        {
            return _unitOfWork.DeliveryOptions.FindAsync(x => globalIds.Contains(x.GlobalId));
        }

        public Task<IEnumerable<DeliveryOption>> GetAllAsync()
        {
            return _unitOfWork.DeliveryOptions.GetAllAsync();
        }

        public Task<IEnumerable<DeliveryOption>> GetAllHiddenAsync()
        {
            return _unitOfWork.DeliveryOptions.FindAsync(x => x.Status == Status.Hidden);
        }

        public Task<IPagedList<DeliveryOption>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<DeliveryOption, bool>> predicate;
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

            return _unitOfWork.DeliveryOptions.GetPagedAsync(
                predicate: predicate,
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection);
        }

        public Task<DeliveryOption> UpdateAsync(DeliveryOption deliveryOption)
        {
            deliveryOption.UpdatedDate = LocalTime.Now();
            _unitOfWork.DeliveryOptions.Update(deliveryOption);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(deliveryOption);
        }

        public Task<List<DeliveryOption>> UpdateStatusRangeAsync(string[] globalIds, Status status)
        {
            var deliveryOptions = _unitOfWork.DeliveryOptions.FindAsync(x => globalIds.Contains(x.GlobalId)).Result.ToList();
            for (int i = 0; i < deliveryOptions.Count; i++)
            {
                deliveryOptions[i].UpdatedDate = LocalTime.Now();
                deliveryOptions[i].Status = status;
            }
            _unitOfWork.DeliveryOptions.UpdateRange(deliveryOptions);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(deliveryOptions);
        }
    }
}
