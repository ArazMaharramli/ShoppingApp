using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShoppingApp.Services.AuthServices.FacebookAuthService.Options;
using ShoppingApp.Services.AuthServices.FacebookAuthService.StaticClasses;
using ShoppingApp.Services.AuthServices.FacebookAuthService.StaticClasses.APIResponceModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.AuthServices.FacebookAuthService
{
    public class FacebookAuthService : IFacebookAuthService
    {
        //private readonly IOptions<FacebookAuthOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FacebookApiUrls _facebookApiUrls;

        public FacebookAuthService(IOptions<FacebookAuthOptions> options, IHttpClientFactory httpClientFactory)
        {
            //_options = options;
            _httpClientFactory = httpClientFactory;
            _facebookApiUrls = new FacebookApiUrls(options: options);
        }

        public async Task<UserInfoFromFacebook> ValidateTokenAndGetUserInfoAsync(string accessToken)
        {
            var tokenValidationResult = await ValideteFacebookAccessTokenAsync(accessToken);

            if (!tokenValidationResult.Data.IsValid)
            {
                return new UserInfoFromFacebook
                {
                    Error = new InternalErrorModel
                    {
                        Code = tokenValidationResult.Data.Error.Code,
                        Type = Utils.Enums.ErrorType.Model,
                        Message = tokenValidationResult.Data.Error.Message,
                    }
                };
            }

            var userInfo = await GetFacebookUserInfoAsync(accessToken);

            return new UserInfoFromFacebook
            {
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Id = userInfo.Id,
                PictureUrl = userInfo.Picture.Data.Url
            };
        }


        #region Privates
        private async Task<FacebookTokenValidationResult> ValideteFacebookAccessTokenAsync(string accesToken)
        {

            var response = await _httpClientFactory.CreateClient().GetAsync(_facebookApiUrls.TokenValidationUrl(accesToken));

            if (response.IsSuccessStatusCode)
            {
                // bura 
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseString);
            }

            return new FacebookTokenValidationResult
            {
                Data = new FacebookTokenValidationData
                {
                    IsValid = false,
                    Error = new FacebookTokenValidationError
                    {
                        Code = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    }
                }
            };
        }

        private async Task<FacebookUserInfoResult> GetFacebookUserInfoAsync(string accesToken)
        {
            var response = await _httpClientFactory.CreateClient().GetAsync(_facebookApiUrls.UserInfoUrl(accesToken));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<FacebookUserInfoResult>(await response.Content.ReadAsStringAsync());
            }

            return new FacebookUserInfoResult
            {
                Error = new FacebookUserInfoError
                {
                    Code = (int)response.StatusCode,
                    Message = response.ReasonPhrase
                }
            };
        }
        #endregion
    }
}
