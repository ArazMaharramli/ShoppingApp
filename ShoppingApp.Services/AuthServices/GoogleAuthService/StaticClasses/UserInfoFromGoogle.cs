using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.AuthServices.GoogleAuthService.StaticClasses
{
    public class UserInfoFromGoogle
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }


        public string Id { get; set; }

        public bool HasError { get; set; } = false;
        public InternalErrorModel Error { get; set; }
    }
}
