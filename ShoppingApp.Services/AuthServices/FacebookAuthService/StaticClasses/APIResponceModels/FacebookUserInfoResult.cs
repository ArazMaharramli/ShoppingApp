using System;
using Newtonsoft.Json;

namespace ShoppingApp.Services.AuthServices.FacebookAuthService.StaticClasses.APIResponceModels
{
    public class FacebookUserInfoResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("picture")]
        public FacebookUserInfoPicture Picture { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("error")]
        public FacebookUserInfoError Error { get; set; }
    }

    public class FacebookUserInfoPicture
    {
        [JsonProperty("data")]
        public PictureData Data { get; set; }
    }

    public class PictureData
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

    }

    public class FacebookUserInfoError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("error_subcode")]
        public long ErrorSubcode { get; set; }

        [JsonProperty("fbtrace_id")]
        public string FbtraceId { get; set; }
    }
}
