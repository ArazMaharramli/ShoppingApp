using System;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;
using ShoppingApp.Repository.Implementation.Repositories.DeliveryRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.DeliveryPersistences
{
    public class DeliveryOptionRepository : Repository<DeliveryOption>, IDeliveryOptionRepository
    {
        public DeliveryOptionRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
