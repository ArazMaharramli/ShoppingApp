using System;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.AuthServices.FacebookAuthService.StaticClasses
{
    public class UserInfoFromFacebook
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }


        public string Id { get; set; }

        public bool HasError { get; set; } = false;
        public InternalErrorModel Error { get; set; }
    }
}
