﻿using System;
namespace ShoppingApp.Web.API.Contracts.RequestModels.V1
{
    public class ResetPasswordRequestModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
