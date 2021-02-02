using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.MediaModels;
using ShoppingApp.Repository.Implementation.Repositories.MediaRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.MediaPersistences
{
    public class UploadedProductMediaForFutureUseRepository : Repository<UploadedProductMediaForFutureUse>, IUploadedProductMediaForFutureUseRepository
    {
        public UploadedProductMediaForFutureUseRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
