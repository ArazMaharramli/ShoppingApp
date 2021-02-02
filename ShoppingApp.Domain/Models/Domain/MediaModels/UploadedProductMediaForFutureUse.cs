using System;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MediaModels
{
    public class UploadedProductMediaForFutureUse : BaseEntitySimple<Status>
    {
        public string MediaUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; } = false;

        public long StoreId { get; set; }
        public Store Store { get; set; }
    }
}
