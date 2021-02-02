using System;
using System.ComponentModel.DataAnnotations;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.UserModels
{
    public class RefreshToken : BaseEntity<RefreshTokenStatus>
    {
        [Key]
        public string Token { get; set; } = Guid.NewGuid().ToString("N");
        public string JwtId { get; set; }
        public DateTime ExpireDate { get; set; }

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
