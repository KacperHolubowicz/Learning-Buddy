using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Auth;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearningBuddy.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand : ICommand<RefreshTokenResponseDTO>
    {
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
            return await GenerateNewToken(request, cancellationToken);
        }

        private IEnumerable<Claim> PrepareClaims(User user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };
        }

        private async Task<RefreshTokenResponseDTO> GenerateNewToken(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            Domain.Users.Entities.RefreshToken refreshToken = await context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(r => r.Value == request.RefreshToken);
            if (refreshToken == null || refreshToken.ExpirationTime <= DateTimeOffset.UtcNow)
            {
                throw new InvalidRefreshTokenException();
            }
            string newAccessToken = tokenService.CreateAccessToken(PrepareClaims(refreshToken.User));
            return new RefreshTokenResponseDTO()
            {
                AccessToken = newAccessToken,
            };
        }
    }
}
