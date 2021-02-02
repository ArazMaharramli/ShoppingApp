using System;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Repository.Implementation.Repositories.AddressRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.AddressPersistences
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
