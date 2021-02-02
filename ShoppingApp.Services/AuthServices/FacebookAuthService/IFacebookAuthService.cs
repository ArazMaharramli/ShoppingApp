using System.Threading.Tasks;
using ShoppingApp.Services.AuthServices.FacebookAuthService.StaticClasses;

namespace ShoppingApp.Services.AuthServices.FacebookAuthService
{
    public interface IFacebookAuthService
    {
        //public Task<FacebookTokenValidationResult> ValideteFacebookAccessTokenAsync(string accesToken);
        //public Task<FacebookUserInfoResult> GetFacebookUserInfoAsync(string accesToken);
        public Task<UserInfoFromFacebook> ValidateTokenAndGetUserInfoAsync(string accessToken);
    }
}
