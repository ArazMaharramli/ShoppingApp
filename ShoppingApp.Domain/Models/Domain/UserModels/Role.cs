using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Domain.Models.Domain.UserModels
{
    public class Role : IdentityRole
    {
        public string IpAddress { get; set; }
    }
    //public class UserSubscriptions : BaseEntitySimple<Status>
    //{
    //    public string UserId { get; set; }
    //    public User User { get; set; }

    //    public Guid SubscriptionIdentifier { get; set; }
    //}
}
