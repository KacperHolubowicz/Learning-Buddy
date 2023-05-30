using FluentValidation;

namespace LearningBuddy.Application.Users.Commands.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login must be provided");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password must be provided");
        }
    }
}
