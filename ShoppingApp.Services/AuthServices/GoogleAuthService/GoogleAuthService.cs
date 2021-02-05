using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingApp.Services.AuthServices.GoogleAuthService.StaticClasses;
using ShoppingApp.Services.AuthServices.GoogleAuthService.StaticClasses.GoogleApiUrls;
using ShoppingApp.Services.AuthServices.GoogleAuthService.Options;
using ShoppingApp.Utils.InternalModels;
using ShoppingApp.Utils.Enums;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ShoppingApp.Services.AuthServices.GoogleAuthService
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<GoogleAuthOptions> _options;

        public GoogleAuthService(IHttpClientFactory httpClientFactory, IOptions<GoogleAuthOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }
        public async Task<UserInfoFromGoogle> ValidateTokenAndGetUserInfo(string token)
        {
            var result = await _httpClientFactory.CreateClient().GetAsync(GoogleApiUrls.TokenValidationAndUserInfoUrl(token));
            if (result.IsSuccessStatusCode)
            {
                GoogleTokenValidationResult validationResult = JsonConvert.DeserializeObject<GoogleTokenValidationResult>(await result.Content.ReadAsStringAsync());
                if (validationResult.Aud != _options.Value.ClientId)
                {
                    return new UserInfoFromGoogle
                    {
                        HasError = true,
                        ErrorType = ErrorType.Model,
                        Errors = new List<InternalErrorModel>{
                            new InternalErrorModel
                            {
                                //ixtiyari verilib http response codlarina uygun
                                Code = 401,
                                Message = "Token is not valid!"
                            }
                        }
                    };
                }

                return new UserInfoFromGoogle
                {
                    FirstName = validationResult.FirstName,
                    LastName = validationResult.LastName,
                    FullName = validationResult.FullName,
                    Email = validationResult.Email,
                    PictureUrl = validationResult.PictureUrl,
                    Id = validationResult.Sub
                };
            }

            return new UserInfoFromGoogle
            {
                HasError = true,
                ErrorType = ErrorType.Server,
                Errors = new List<InternalErrorModel>{
                    new InternalErrorModel
                    {
                        Code = 401,
                        Message = "Google Server did not respond."
                    }
                }
            };
        }
    }
}
