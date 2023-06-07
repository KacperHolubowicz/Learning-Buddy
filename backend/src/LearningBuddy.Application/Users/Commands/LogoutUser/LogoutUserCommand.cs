using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Users.Commands.LogoutUser
{
    public record LogoutUserCommand : ICommand<bool>
    {
        public string TokenValue { get; set; }
    }

    public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand, bool>
    {
        private readonly IUsersDbContext uContext;

        public LogoutUserCommandHandler(IUsersDbContext uContext)
        {
            this.uContext = uContext;
        }

        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Users.Entities.RefreshToken token = await uContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.Value == request.TokenValue);
            if(token != null)
            {
                uContext.RefreshTokens.Remove(token);
                await uContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }
}
