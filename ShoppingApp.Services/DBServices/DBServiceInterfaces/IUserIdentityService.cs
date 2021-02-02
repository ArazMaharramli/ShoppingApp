using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IUserIdentityService
    {
        public Task<User> GetUserInfoAsync(string Id);
        public Task<IList<Claim>> GetClaimsAsync(User user);
        public Task<IList<string>> GetRolesAsync(User user);

        //public Task<IEnumerable<User>> GetUsersAsync(int pageNumber, int pageCapacity);
        //public Task<User> FindByLoginAsync(string providerName, string key);
        //public Task<>

        //public Task<UserInfoModel> CreateUser(CreateUserRequestModel model);
        public Task<RefreshTokenResponseModel> UpdateRefreshTokenAsync(string oldRefreshToken, string oldJwtId, string newJwtId);
        public Task<RefreshTokenResponseModel> CreateRefreshTokenAsync(string userId, string jwtId);


    }
}