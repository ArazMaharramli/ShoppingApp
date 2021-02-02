using System;
using System.Threading.Tasks;
using ShoppingApp.Services.AuthServices.GoogleAuthService.StaticClasses;

namespace ShoppingApp.Services.AuthServices.GoogleAuthService
{
    public interface IGoogleAuthService
    {
        public Task<UserInfoFromGoogle> ValidateTokenAndGetUserInfo(string Token);
    }
}
