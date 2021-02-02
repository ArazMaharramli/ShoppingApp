using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Repository.Implementation.Repositories.StoreRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.StorePersistences
{
    public class StoreContactRepository : Repository<StoreContact>, IStoreContactRepository
    {
        public StoreContactRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
