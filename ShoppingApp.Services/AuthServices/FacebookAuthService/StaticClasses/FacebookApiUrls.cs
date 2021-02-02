using System;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.AuthServices.FacebookAuthService.Options;

namespace ShoppingApp.Services.AuthServices.FacebookAuthService.StaticClasses
{
    public class FacebookApiUrls
    {
        private IOptions<FacebookAuthOptions> _options { get; set; }

        public static string BaseUrl = @"https://graph.facebook.com/";

        public string TokenValidationUrl(string accesstoken)
        {
            return String.Format(@"{0}{1}/debug_token?input_token={2}&access_token={3}|{4}", BaseUrl, _options.Value.APIVersion, accesstoken, _options.Value.AppId, _options.Value.AppSecret);
        }

        public string UserInfoUrl(string accessToken)
        {
            return string.Format(@"{0}me?fields={1}&access_token={2}", BaseUrl, _options.Value.UserInfoFields, accessToken);
        }



        public FacebookApiUrls(IOptions<FacebookAuthOptions> options)
        {
            _options = options;
        }
    }
}

