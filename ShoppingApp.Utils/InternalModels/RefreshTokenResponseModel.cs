using System;

namespace ShoppingApp.Utils.InternalModels
{
    public class RefreshTokenResponseModel : BaseResponseModel
    {
        public string JwtId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
