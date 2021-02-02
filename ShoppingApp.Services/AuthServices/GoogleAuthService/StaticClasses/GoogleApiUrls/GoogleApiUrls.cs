using System;
namespace ShoppingApp.Services.AuthServices.GoogleAuthService.StaticClasses.GoogleApiUrls
{
    public class GoogleApiUrls
    {
        private const string _tokenValidationAndUserInfoUrl = @"https://oauth2.googleapis.com/tokeninfo?id_token={0}";

        public static string TokenValidationAndUserInfoUrl(string token)
        {
            return string.Format(_tokenValidationAndUserInfoUrl, token);
        }
    }
}
