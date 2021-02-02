using System;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.MediaModels;
using ShoppingApp.Repository.Implementation.Repositories.MediaRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.MediaPersistences
{
    public class PhotoFrameRepository : Repository<PhotoFrame>, IPhotoFrameRepository
    {
        public PhotoFrameRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext notsetDbContext => Context as ShoppingAppDbContext;
    }
}
