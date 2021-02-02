using System.Collections.Generic;
using System.Security.Claims;

namespace ShoppingApp.Utils.InternalModels
{
    public class UserInfoModel
    {
        public string UserId { get; set; }
        public IEnumerable<Claim> UserClaims { get; set; } = new List<Claim>();

        public bool HasError { get; set; }
        public List<InternalErrorModel> Errors { get; set; }
    }
}
