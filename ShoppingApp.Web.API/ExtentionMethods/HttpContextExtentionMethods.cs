using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Web.API.ExtentionMethods
{
    public static class HttpContextExtentionMethods
    {
        public static string GetJwt(this HttpContext httpContext)
        {

            httpContext.Request.Headers.TryGetValue("authorization", out var value);
            if (value.FirstOrDefault() != null)
            {
                return value.FirstOrDefault().Split(" ").Last();
            }

            return null;
        }
    }
}
