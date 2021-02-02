using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Models.Domain.OrderModels;
using ShoppingApp.Domain.Models.Domain.ShoppingCartModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;

namespace ShoppingApp.Domain.Models.Domain.UserModels
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; } = new List<IdentityUserLogin<string>>();

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ProfilePhoto { get; set; }
        

        public long UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public long? StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<UserNotificationToken> NotificationTokens { get; set; } = new HashSet<UserNotificationToken>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new HashSet<ShoppingCart>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<UserContact> UserContacts { get; set; } = new HashSet<UserContact>();
    }   
}