using System;
namespace ShoppingApp.Web.API.ApiRoutes.v1
{
    public static class APIRoutes
    {
        public const string Root = "api";
        public const string Version = "/v1";
        public const string Base = Root + Version;

        public static class Account
        {
            public const string ControllerName = "/auth";
            public const string Login = Base + ControllerName + "/login";
            public const string LoginWithFacebook = Base + ControllerName + "/login/facebook";
            public const string LoginWithGoogle = Base + ControllerName + "/login/google";

            public const string RefreshToken = Base + ControllerName + "/refreshtoken";

            //?????? sorgu mentiqini dusunmek lazimdi
            public const string LoginWithPhoneNumber = Base + ControllerName + "/login/phonenumber";
            public const string Register = Base + ControllerName + "/register";
            public const string ForgotPassword = Base + ControllerName + "/forgotpassword";
            public const string ResetPassword = Base + ControllerName + "/resetpassword";
        }
    }
}
