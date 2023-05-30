using FluentValidation;

namespace LearningBuddy.Application.Users.Commands.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username must be provided")
                .Length(5, 30)
                .WithMessage("Username should have between 5 and 30 characters");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email must be provided")
                .EmailAddress()
                .WithMessage("It is not a valid email address");
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login must be provided")
                .Length(5, 30)
                .WithMessage("Login should have between 5 and 30 characters");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password must be provided")
                .Length(10, 30)
                .WithMessage("Password should have between 10 and 30 characters")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                .WithMessage("You must provide repeated password")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
