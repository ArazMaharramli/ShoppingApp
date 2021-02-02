using System;
namespace ShoppingApp.Utils.InternalModels
{
    public class JwtTokenModel
    {
        public string JwtToken { get; set; }
        public string TokenId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
