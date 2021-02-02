using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.StoreModels
{
    public class StoreContact : BaseEntitySimple<Status>
    {
        public string PhoneNumber { get; set; }

        public long StoreId { get; set; }
        public Store Store { get; set; }
    }
}
