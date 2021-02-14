using System;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels
{
    public class RefreshTokenCommandResponseModel : BaseResponseModel
    {
        public string RefreshToken { get; set; }
        public string Jwt { get; set; }
        public DateTime JwtExpirationDate { get; set; }
    }

}
