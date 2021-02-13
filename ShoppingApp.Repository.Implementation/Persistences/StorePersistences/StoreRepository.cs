using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Repository.Implementation.Repositories.StoreRepositories;
using ShoppingApp.Repository.Persistences;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Repository.Implementation.Persistences.StorePersistences
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
