using LearningBuddy.Application.Common.Interfaces.Auth;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LearningBuddy.Infrastructure.Auth
{
    public class TokenService : ITokenService
    {
        private readonly string jwtKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly int accessTokenMinutes;
        private readonly int refreshTokenDays;

        public TokenService(IConfiguration configuration)
        {
            jwtKey = configuration["Security:JwtKey"];
            issuer = configuration["Security:Issuer"];
            audience = configuration["Security:Audience"];
            accessTokenMinutes = int.Parse(configuration["Security:AccessTokenMinutes"]);
            refreshTokenDays = int.Parse(configuration["Security:RefreshTokenDays"]);
        }

        public string CreateAccessToken(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken newToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(accessTokenMinutes),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(newToken);
            return tokenString;
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string convertedToken = Convert.ToBase64String(randomNumber);
                return new RefreshToken()
                {
                    Value = convertedToken,
                    ExpirationTime = DateTimeOffset.UtcNow.AddDays(refreshTokenDays)
                };
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateLifetime = false,
                ValidIssuer = issuer,
                ValidAudience = audience
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
