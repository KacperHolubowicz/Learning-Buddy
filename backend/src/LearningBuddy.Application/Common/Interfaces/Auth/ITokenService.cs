using LearningBuddy.Domain.Users.Entities;
using System.Security.Claims;

namespace LearningBuddy.Application.Common.Interfaces.Auth
{
    public interface ITokenService
    {
        public string CreateAccessToken(IEnumerable<Claim> claims);
        public RefreshToken CreateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
