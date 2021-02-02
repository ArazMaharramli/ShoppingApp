using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.UserModels
{
    public class UserContact : BaseEntitySimple<Status>
    {
        public string Value { get; set; }
        public ContactType ContactType { get; set; } = ContactType.Phone;

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
//public class UserSubscriptions : BaseEntitySimple<Status>
//{
//    public string UserId { get; set; }
//    public User User { get; set; }

//    public Guid SubscriptionIdentifier { get; set; }
//}
