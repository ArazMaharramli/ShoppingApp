using System;
using System.Threading.Tasks;
using ShoppingApp.Repository.Implementation.Repositories.AddressRepositories;
using ShoppingApp.Repository.Implementation.Repositories.AdvertisementRepositories;
using ShoppingApp.Repository.Implementation.Repositories.DeliveryRepositories;
using ShoppingApp.Repository.Implementation.Repositories.MediaRepositories;
using ShoppingApp.Repository.Implementation.Repositories.OrderRepositories;
using ShoppingApp.Repository.Implementation.Repositories.PaymentRepositories;
using ShoppingApp.Repository.Implementation.Repositories.ProductRepositories;
using ShoppingApp.Repository.Implementation.Repositories.ShoppingCartRepositories;
using ShoppingApp.Repository.Implementation.Repositories.StoreRepositories;
using ShoppingApp.Repository.Implementation.Repositories.UserRepositories;

namespace ShoppingApp.UnitOFWork.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
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
        public ISizeTypeRepository SizeTypes { get; }
        public ITagRepository Tags { get; }

        public IShoppingCartItemRepository ShoppingCartItems { get; }
        public IShoppingCartRepository ShoppingCarts { get; }

        public IStoreContactRepository StoreContacts { get; }
        public IStoreRepository Stores { get; }
        public IStoreTypeRepository StoreTypes { get; }

        public IRefreshTokenRepository RefreshTokens { get; }
        public IUserContactRepository UserContacts { get; }
        public IUserNotificationTokenRepository UserNotificationTokens { get; }
        public IUserTypeRepository UserTypes { get; }
        public IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
