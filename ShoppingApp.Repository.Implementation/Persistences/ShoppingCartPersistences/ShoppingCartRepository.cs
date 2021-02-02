using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.ShoppingCartModels;
using ShoppingApp.Repository.Implementation.Repositories.ShoppingCartRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.ShoppingCartPersistences
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
