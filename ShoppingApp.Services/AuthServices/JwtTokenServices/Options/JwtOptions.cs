using System;
namespace ShoppingApp.Services.AuthServices.JwtTokenServices.Options
{
    public class JwtOptions
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecurityKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }

        public TimeSpan TokenLifeTime { get; set; }
    }
}
