using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.CreateLearningSource
{
    public class CreateLearningSourceValidator : AbstractValidator<CreateLearningSourceCommand>
    {
        public CreateLearningSourceValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
            RuleFor(x => x.SubjectID)
                .NotEmpty()
                .WithMessage("Subject ID must be provided");
            RuleFor(x => x.Public)
                .NotEmpty()
                .WithMessage("Must explicitly provide value for 'Public'");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name must be provided")
                .Length(3, 30)
                .WithMessage("Name must have between 3 and 30 characters");
            When(x => !string.IsNullOrEmpty(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(200)
                    .WithMessage("Description must not have more than 200 characters");
            });
        }
    }
}
