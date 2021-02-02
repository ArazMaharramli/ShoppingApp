using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.UserModels
{
    public class UserNotificationToken : BaseEntitySimple<Status>
    {
        public string DeviceInfo { get; set; }

        public string NotificationToken { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
    //public class UserSubscriptions : BaseEntitySimple<Status>
    //{
    //    public string UserId { get; set; }
    //    public User User { get; set; }

    //    public Guid SubscriptionIdentifier { get; set; }
    //}
}
