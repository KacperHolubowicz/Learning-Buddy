using FluentValidation;

namespace LearningBuddy.Application.Users.Commands.LogoutUser
{
    public class LogoutUserValidator : AbstractValidator<LogoutUserCommand>
    {
        public LogoutUserValidator()
        {
            RuleFor(c => c.TokenValue)
                .NotEmpty()
                .WithMessage("Refresh token must be provided");
        }
    }
}
