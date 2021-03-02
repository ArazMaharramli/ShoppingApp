using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Domain.Models.Domain.MediaModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.StoreModels
{
    public class Store : BaseEntitySimple<StoreStatus>
    {
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string UniqueSlug { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }

        public string ProfilePhotoUrl { get; set; }
        public string CoverPhotoUrl { get; set; }

        public long StoreTypeId { get; set; }
        public StoreType StoreType { get; set; }

        public long AddressId { get; set; }
        public Address Address { get; set; }

        public long? PhotoFrameId { get; set; }
        public PhotoFrame PhotoFrame { get; set; }

        public User Owner { get; set; }

        // companycontact,companypaymentoption
        public ICollection<StoreContact> StoreContacts { get; set; } = new HashSet<StoreContact>();
        public ICollection<StorePaymentOption> StorePaymentOptions { get; set; } = new HashSet<StorePaymentOption>();
        public ICollection<UploadedProductMediaForFutureUse> UploadedProductMediasForFutureUse { get; set; } = new HashSet<UploadedProductMediaForFutureUse>();
        public ICollection<StoreDeliveryOption> StoreDeliveryOptions { get; set; } = new HashSet<StoreDeliveryOption>();
        public ICollection<Product> Products { get; set; }

    }
}
