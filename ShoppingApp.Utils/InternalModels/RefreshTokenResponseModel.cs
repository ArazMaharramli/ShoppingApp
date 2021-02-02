using System;

namespace ShoppingApp.Utils.InternalModels
{
    public class RefreshTokenResponseModel
    {
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }

        public bool HasError { get; set; }
        public InternalErrorModel Error { get; set; }
    }
}
