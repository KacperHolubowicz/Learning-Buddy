using FluentValidation;

namespace LearningBuddy.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty()
                .WithMessage("Access token must be provided");
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token must be provided");
        }
    }
}
