using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Auth;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Users.Commands.LoginUser;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearningBuddy.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand : ICommand<RefreshTokenResponseDTO>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenResponseDTO>
    {
        private readonly IUsersDbContext context;
        private readonly ITokenService tokenService;

        public RefreshTokenCommandHandler(IUsersDbContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        public async Task<RefreshTokenResponseDTO> Handle
            (RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            long userId = int.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = await context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.ID == userId);
            if(user == null) 
            {
                throw new ResourceNotFoundException("User", userId);
            }
            return await GenerateNewToken(request, user, cancellationToken);
        }

        private IEnumerable<Claim> PrepareClaims(User user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };
        }

        private async Task<RefreshTokenResponseDTO> GenerateNewToken(RefreshTokenCommand request, 
            User user, CancellationToken cancellationToken)
        {
            Domain.Users.Entities.RefreshToken refreshToken = user.RefreshTokens
                .FirstOrDefault(r => r.Value == request.RefreshToken);
            if (refreshToken == null || refreshToken.ExpirationTime <= DateTimeOffset.UtcNow)
            {
                throw new InvalidRefreshTokenException();
            }
            string newAccessToken = tokenService.CreateAccessToken(PrepareClaims(user));
            return new RefreshTokenResponseDTO()
            {
                AccessToken = newAccessToken,
            };
        }
    }
}
