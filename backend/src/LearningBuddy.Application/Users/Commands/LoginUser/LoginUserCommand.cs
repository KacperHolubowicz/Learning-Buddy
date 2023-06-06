using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Auth;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Common.Interfaces.Security;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearningBuddy.Application.Users.Commands.LoginUser
{
    public record LoginUserCommand : ICommand<TokenResponseDTO>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenResponseDTO>
    {
        private readonly IUsersDbContext context;
        private readonly ITokenService tokenService;
        private readonly IEncryptionService encryptionService;

        public LoginUserCommandHandler(IUsersDbContext context, ITokenService tokenService,
            IEncryptionService encryptionService)
        {
            this.context = context;
            this.tokenService = tokenService;
            this.encryptionService = encryptionService;
        }
        public async Task<TokenResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await context.Users
                    .Include(u => u.RefreshTokens)
                    .FirstAsync(u => u.Login == request.Login);
                byte[] salt = user.Salt;
                byte[] userPassword = user.Password;
                if (!encryptionService.CheckPassword(request.Password, salt, userPassword))
                {
                    throw new ArgumentNullException();
                }
                return await CreateTokenResponse(cancellationToken, user);
            }
            catch (ArgumentNullException)
            {
                throw new ResourceNotFoundException("Incorrect login or password");
            }
        }

        private IEnumerable<Claim> PrepareClaims(User user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };
        }

        private async Task<TokenResponseDTO> CreateTokenResponse(CancellationToken ct, User user)
        {
            Domain.Users.Entities.RefreshToken newToken = tokenService.CreateRefreshToken();
            user.RefreshTokens.Add(newToken);
            context.Users.Update(user);
            await context.SaveChangesAsync(ct);
            return new TokenResponseDTO
            {
                AccessToken = tokenService.CreateAccessToken(PrepareClaims(user)),
                RefreshToken = newToken.Value,
                UserUsername = user.Username
            };
        }
    }
}
