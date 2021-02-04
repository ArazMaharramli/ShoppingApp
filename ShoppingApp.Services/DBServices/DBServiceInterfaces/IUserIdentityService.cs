using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IUserIdentityService
    {
        public Task<User> FindByIdAsync(string Id);
        public Task<User> FindByEmailAsync(string email);

        public Task<IList<Claim>> GetClaimsAsync(User user);
        public Task<IList<string>> GetRolesAsync(User user);
        public Task<string> GeneratePasswordResetTokenAsync(User user);
        public Task<bool> IsEmailConfirmedAsync(User user);

        public Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo);
        public Task<IdentityResult> CreateAsync(User user, string password = null);
        public Task<bool> CheckPasswordAsync(User user, string password);

        public Task<User> FindByLoginAsync(string providerName, string key);

        public Task<RefreshTokenResponseModel> UpdateRefreshTokenAsync(string oldRefreshToken, string oldJwtId, string newJwtId);
        public Task<RefreshTokenResponseModel> CreateRefreshTokenAsync(string userId, string jwtId);


    }
}