using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.MediaModels;
using ShoppingApp.Repository.Implementation.Repositories.MediaRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.MediaPersistences
{
    public class ProductMediaRepository : Repository<ProductMedia>, IProductMediaRepository
    {
        public ProductMediaRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
