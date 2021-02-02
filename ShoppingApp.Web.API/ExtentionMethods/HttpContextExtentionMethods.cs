using System.Linq;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Web.API.ExtentionMethods
{
    public static class HttpContextExtentionMethods
    {
        public static UserInfoModel GetUserInfo(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return new UserInfoModel
                {
                    UserId = string.Empty,
                };
            }

            return new UserInfoModel{
                UserId = httpContext.User.Claims.Single(x => x.Type == "Id").Value,
            };
            
        }
    }
}
