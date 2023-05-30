using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.CreateSubjectTask
{
    public class CreateSubjectTaskValidator : AbstractValidator<CreateSubjectTaskCommand>
    {
        public CreateSubjectTaskValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name must be provided")
                .Length(3, 30)
                .WithMessage("Name must have between 3 and 30 characters");
            When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(200)
                    .WithMessage("Description must not have more than 200 characters");
            });
            RuleFor(x => x.Difficulty)
                .NotEmpty()
                .WithMessage("Difficulty must be provided")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Difficulty must be greater or equal to 1")
                .LessThanOrEqualTo(5)
                .WithMessage("Difficulty must be less or equal to 5");
            RuleFor(x => x.Priority)
                .NotEmpty()
                .WithMessage("Priority must be provided")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Priotiy must be greater or equal to 1")
                .LessThanOrEqualTo(5)
                .WithMessage("Priority must be less or equal to 5");
            When(x => x.Deadline != null, () =>
            {
                RuleFor(x => x.Deadline)
                    .GreaterThan(DateTimeOffset.UtcNow)
                    .WithMessage("Deadline must be in the future");
            });
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
            RuleFor(x => x.SubjectID)
                .NotEmpty()
                .WithMessage("Subject ID must be provided");
        }
    }
}
