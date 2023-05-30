using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.UpdateLearningSource
{
    public class UpdateLearningSourceValidator : AbstractValidator<UpdateLearningSourceCommand>
    {
        public UpdateLearningSourceValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
            RuleFor(x => x.SourceID)
                .NotEmpty()
                .WithMessage("Source ID must be provided");
            When(x => !string.IsNullOrEmpty(x.Name), () =>
            {
                RuleFor(x => x.Name)
                .Length(3, 30)
                .WithMessage("Name must have between 3 and 30 characters");
            });
            When(x => !string.IsNullOrEmpty(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(200)
                    .WithMessage("Description must not have more than 200 characters");
            });
        }
    }
}
