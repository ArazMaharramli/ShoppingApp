using System;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.ShoppingCartModels;
using ShoppingApp.Repository.Implementation.Repositories.ShoppingCartRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.ShoppingCartPersistences
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
