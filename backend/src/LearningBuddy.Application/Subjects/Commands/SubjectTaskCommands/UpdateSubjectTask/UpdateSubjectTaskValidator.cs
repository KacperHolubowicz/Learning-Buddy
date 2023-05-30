using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.UpdateSubjectTask
{
    public class UpdateSubjectTaskValidator : AbstractValidator<UpdateSubjectTaskCommand>
    {
        public UpdateSubjectTaskValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Name must be provided")
                    .Length(3, 30)
                    .WithMessage("Name must have between 3 and 30 characters");
            });
            When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(200)
                    .WithMessage("Description must not have more than 200 characters");
            });
            When(x => x.Difficulty != null, () =>
            {
                RuleFor(x => x.Difficulty)
                    .NotEmpty()
                    .WithMessage("Difficulty must be provided")
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Difficulty must be greater or equal to 1")
                    .LessThanOrEqualTo(5)
                    .WithMessage("Difficulty must be less or equal to 5");
            });
            When(x => x.Difficulty != null, () =>
            {
                RuleFor(x => x.Difficulty)
                    .NotEmpty()
                    .WithMessage("Difficulty must be provided")
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Difficulty must be greater or equal to 1")
                    .LessThanOrEqualTo(5)
                    .WithMessage("Difficulty must be less or equal to 5");
            });
            When(x => x.Priority != null, () =>
            {
                RuleFor(x => x.Priority)
                    .NotEmpty()
                    .WithMessage("Priority must be provided")
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Priotiy must be greater or equal to 1")
                    .LessThanOrEqualTo(5)
                    .WithMessage("Priority must be less or equal to 5");
            });
            When(x => x.Deadline != null, () =>
            {
                RuleFor(x => x.Deadline)
                    .GreaterThan(DateTimeOffset.Now)
                    .WithMessage("Deadline must be in the future");
            });
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
            RuleFor(x => x.SubjectTaskID)
                .NotEmpty()
                .WithMessage("Subject task ID must be provided");
        }
    }
}
