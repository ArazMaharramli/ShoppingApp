using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.AdvertisementModels;
using ShoppingApp.Repository.Implementation.Repositories.AdvertisementRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.AdvertisementPersistences
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
