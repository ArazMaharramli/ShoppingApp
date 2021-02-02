using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.AuthServices.JwtTokenServices
{
    public interface IJwtTokenService
    {
        public JwtTokenModel CreateNewJwtToken(User user, IEnumerable<Claim> userClaims, string tokenId);
        public bool IsJwtWithValidSecurityAlgorithm(SecurityToken token);
        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
        public string GetTokenId(string token);
        public string GetUserId(string token);
    }
}
