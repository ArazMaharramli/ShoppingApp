using System;
using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.UserModels;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class LoginAndRegisterCommandsResponseModel : BaseResponseModel
    {
        public User User { get; set; }
        public string JwtToken { get; set; }
        public DateTime JwtExpirationDate { get; set; }

        public string RefreshToken { get; set; }

    }
}
