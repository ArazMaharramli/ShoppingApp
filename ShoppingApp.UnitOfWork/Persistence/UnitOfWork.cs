using System;
using System.Threading.Tasks;
using ShoppingApp.Domain.Data;
using ShoppingApp.Repository.Implementation.Persistences.AddressPersistences;
using ShoppingApp.Repository.Implementation.Repositories.AddressRepositories;

using ShoppingApp.Repository.Implementation.Repositories.AdvertisementRepositories;
using ShoppingApp.Repository.Implementation.Persistences.AdvertisementPersistences;

using ShoppingApp.Repository.Implementation.Repositories.DeliveryRepositories;
using ShoppingApp.Repository.Implementation.Persistences.DeliveryPersistences;

using ShoppingApp.Repository.Implementation.Repositories.MediaRepositories;
using ShoppingApp.Repository.Implementation.Persistences.MediaPersistences;

using ShoppingApp.Repository.Implementation.Repositories.OrderRepositories;
using ShoppingApp.Repository.Implementation.Persistences.OrderPersistences;

using ShoppingApp.Repository.Implementation.Repositories.PaymentRepositories;
using ShoppingApp.Repository.Implementation.Persistences.PaymentPersistences;

using ShoppingApp.Repository.Implementation.Repositories.ProductRepositories;
using ShoppingApp.Repository.Implementation.Persistences.ProductPersistences;

using ShoppingApp.Repository.Implementation.Repositories.ShoppingCartRepositories;
using ShoppingApp.Repository.Implementation.Persistences.ShoppingCartPersistences;

using ShoppingApp.Repository.Implementation.Repositories.StoreRepositories;
using ShoppingApp.Repository.Implementation.Persistences.StorePersistences;

using ShoppingApp.Repository.Implementation.Repositories.UserRepositories;
using ShoppingApp.Repository.Implementation.Persistences.UserPersistences;
using ShoppingApp.UnitOFWork.Repositories;

namespace ShoppingApp.UnitOFWork.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingAppDbContext _context;

        public UnitOfWork(ShoppingAppDbContext context)
        {
            _context = context;

            Addresses = new AddressRepository(_context);
            Cities = new CityRepository(_context);
            Countries = new CountryRepository(_context);

            Advertisements = new AdvertisementRepository(_context);

            DeliveryOptions = new DeliveryOptionRepository(_context);

            PhotoFrames = new PhotoFrameRepository(_context);
            ProductMedias = new ProductMediaRepository(_context);
            UploadedProductMediasForFutureUse = new UploadedProductMediaForFutureUseRepository(_context);

            OrderItemNotes = new OrderItemNoteRepository(_context);
            OrderItems = new OrderItemRepository(_context);
            Orders = new OrderRepository(_context);

            PaymentOptions = new PaymentOptionRepository(_context);

            Brands = new BrandRepository(_context);
            Categories = new CategoryRepository(_context);
            Colors = new ColorRepository(_context);
            Materials = new MaterialRepository(_context);
            ProductDetails = new ProductDetailRepository(_context);
            Products = new ProductRepository(_context);
            Sizes = new SizeRepository(_context);
            Tags = new TagRepository(_context);

            ShoppingCartItems = new ShoppingCartItemRepository(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);

            StoreContacts = new StoreContactRepository(_context);
            Stores = new StoreRepository(_context);
            StoreTypes = new StoreTypeRepository(_context);

            RefreshTokens = new RefreshTokenRepository(_context);
            UserNotificationTokens = new UserNotificationTokenRepository(_context);
            UserContacts = new UserContactRepository(_context);
            Users = new UserRepository(_context);
        }

        public IAddressRepository Addresses { get; }
        public ICityRepository Cities { get; }
        public ICountryRepository Countries { get; }

        public IAdvertisementRepository Advertisements { get; }

        public IDeliveryOptionRepository DeliveryOptions { get; }

        public IPhotoFrameRepository PhotoFrames { get; }
        public IProductMediaRepository ProductMedias { get; }
        public IUploadedProductMediaForFutureUseRepository UploadedProductMediasForFutureUse { get; }

        public IOrderItemNoteRepository OrderItemNotes { get; }
        public IOrderItemRepository OrderItems { get; }
        public IOrderRepository Orders { get; }

        public IPaymentOptionRepository PaymentOptions { get; }

        public IBrandRepository Brands { get; }
        public ICategoryRepository Categories { get; }
        public IColorRepository Colors { get; }
        public IMaterialRepository Materials { get; }
        public IProductDetailRepository ProductDetails { get; }
        public IProductRepository Products { get; }
        public ISizeRepository Sizes { get; }
        public ITagRepository Tags { get; }

        public IShoppingCartItemRepository ShoppingCartItems { get; }
        public IShoppingCartRepository ShoppingCarts { get; }

        public IStoreContactRepository StoreContacts { get; }
        public IStoreRepository Stores { get; }
        public IStoreTypeRepository StoreTypes { get; }

        public IRefreshTokenRepository RefreshTokens { get; }
        public IUserContactRepository UserContacts { get; }
        public IUserNotificationTokenRepository UserNotificationTokens { get; }
        public IUserRepository Users { get; }

        public void Dispose()
        {
            _context.Dispose();
        }


        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
