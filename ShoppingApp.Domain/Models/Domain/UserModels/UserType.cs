using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.UserModels
{
    public class UserType : BaseEntitySimple<Status>
    {
        public string UniqueName { get; set; }

        public ICollection<User> Users { get; set; }
    }
    //public class UserSubscriptions : BaseEntitySimple<Status>
    //{
    //    public string UserId { get; set; }
    //    public User User { get; set; }

    //    public Guid SubscriptionIdentifier { get; set; }
    //}
}
