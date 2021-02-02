﻿using System;
using System.Collections.Generic;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class RefreshTokenCommandResponseModel : BaseResponseModel
    {
        public string RefreshToken { get; set; }
        public string Jwt { get; set; }
        public DateTime JwtExpirationDate { get; set; }
    }

}
