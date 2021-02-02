using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.AuthServices.JwtTokenServices.Options;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.AuthServices.JwtTokenServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IOptions<JwtOptions> _options;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _options = options;
        }

        public JwtTokenModel CreateNewJwtToken(User user, IEnumerable<Claim> userClaims, string tokenId)
        {

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim("Id", user.Id)
            };

            if (userClaims != null)
            {
                authClaims.AddRange(userClaims);
            }


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecurityKey));
            var expirationDate = DateTime.UtcNow.Add(_options.Value.TokenLifeTime);
            var token = new JwtSecurityToken(
                audience: _options.Value.ValidAudience,
                issuer: _options.Value.ValidIssuer,
                expires: expirationDate,
                notBefore: DateTime.UtcNow,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtTokenModel
            {
                JwtToken = tokenString,
                TokenId = tokenId,
                ExpirationDate = expirationDate,

            };
        }

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenValdidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = _options.Value.ValidateIssuer,
                ValidateAudience = _options.Value.ValidateAudience,
                ValidAudience = _options.Value.ValidAudience,
                ValidIssuer = _options.Value.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.SecurityKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValdidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetTokenId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {
                return new JwtSecurityToken(token).Id;
            }

            return null;
        }

        public string GetUserId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {
                return new JwtSecurityToken(token).Subject;
            }

            return null;
        }

        public bool IsJwtWithValidSecurityAlgorithm(SecurityToken securityToken)
        {

            return (securityToken is JwtSecurityToken jwtSecurityToken) &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                    StringComparison.InvariantCultureIgnoreCase);

        }
    }
}
