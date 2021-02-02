using System;
namespace ShoppingApp.Web.API.Contracts.RequestModels.V1
{
    public class RefreshTokenRequestModel
    {

        public string RefreshToken { get; set; }
        public string JwtToken { get; set; }
    }
}
