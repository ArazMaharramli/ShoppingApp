using System;

namespace ShoppingApp.Web.API.Contracts.ResponseModels.V1
{
    public class RefreshAndJwtTokenResponseModel
    {
        public string JwtToken { get; set; }
        public DateTime JwtExpirationDate { get; set; }

        public string RefreshToken { get; set; }

        public string Provider { get; set; } = "shoppingapp";
    }
}
