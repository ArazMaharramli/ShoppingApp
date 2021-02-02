using System;
using Newtonsoft.Json;

namespace ShoppingApp.Services.AuthServices.GoogleAuthService.StaticClasses
{

    public  class GoogleTokenValidationResult
        {
             [JsonProperty("email")]
             public string Email { get; set; }

             [JsonProperty("email_verified")]
             public bool? EmailVerified { get; set; }

             [JsonProperty("name")]
             public string FullName { get; set; }

             [JsonProperty("picture")]
             public string PictureUrl { get; set; }

             [JsonProperty("given_name")]
             public string FirstName { get; set; }

             [JsonProperty("family_name")]
             public string LastName { get; set; }

             [JsonProperty("locale")]
             public string Locale { get; set; }



             [JsonProperty("iss")]
             public string Iss { get; set; }

             [JsonProperty("sub")]
             public string Sub { get; set; }

             [JsonProperty("azp")]
             public string Azp { get; set; }

             [JsonProperty("aud")]
             public string Aud { get; set; }

             [JsonProperty("iat")]
             public long? Iat { get; set; }

             [JsonProperty("exp")]
             public long? Exp { get; set; }

             [JsonProperty("jti")]
             public string Jti { get; set; }

             [JsonProperty("alg")]
             public string Alg { get; set; }

             [JsonProperty("kid")]
             public string Kid { get; set; }

             [JsonProperty("typ")]
             public string Typ { get; set; }

             [JsonProperty("at_hash")]
             public string AtHash { get; set; }
    }
}
