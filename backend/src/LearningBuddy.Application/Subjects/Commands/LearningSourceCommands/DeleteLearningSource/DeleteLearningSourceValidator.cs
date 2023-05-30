using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.DeleteLearningSource
{
    public class DeleteLearningSourceValidator : AbstractValidator<DeleteLearningSourceCommand>
    {
        public DeleteLearningSourceValidator()
        {
            RuleFor(x => x.SourceID)
                .NotEmpty()
                .WithMessage("Learning source ID must be provided");
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
        }
    }
}
