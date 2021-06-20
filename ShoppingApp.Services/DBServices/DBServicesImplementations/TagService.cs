using System;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tag> CreateAsync(string name)
        {
            var tagInDb = await _unitOfWork.Tags.GetAsync(x => x.UniqueName.ToLower() == name.ToLower().Trim() && x.Status == Utils.Enums.Status.Active);
            if (tagInDb is null)
            {
                var tag = new Tag { UniqueName = name.Trim().ToLower() };
                _unitOfWork.Tags.Add(tag);
                await _unitOfWork.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<Tag> GetByName(string name)
        {
            return await _unitOfWork.Tags.GetAsync(x => x.UniqueName.ToLower() == name.ToLower().Trim() && x.Status == Utils.Enums.Status.Active);

        }
    }
}
