using System;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels
{
    public class LoginAndRegisterCommandsResponseModel : BaseResponseModel
    {
        public User User { get; set; }
        public string JwtToken { get; set; }
        public DateTime JwtExpirationDate { get; set; }

        public string RefreshToken { get; set; }

    }
}
