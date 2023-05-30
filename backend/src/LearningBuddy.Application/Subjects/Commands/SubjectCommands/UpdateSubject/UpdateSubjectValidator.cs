using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.SubjectCommands.UpdateSubject
{
    public class UpdateSubjectValidator : AbstractValidator<UpdateSubjectCommand>
    {
        public UpdateSubjectValidator()
        {
            RuleFor(x => x.SubjectID)
                .NotEmpty()
                .WithMessage("Subject ID must be provided");
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
            When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(200)
                    .WithMessage("Description must not be longer than 200 characters");
            });
            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name)
                    .Length(3, 30)
                    .WithMessage("Name must have between 3 and 30 characters");
            });
            When(x => x.Tags != null && x.Tags.Count() > 0, () =>
            {
                RuleForEach(x => x.Tags)
                    .Length(2, 30)
                    .WithMessage("Tag must have between 2 and 30 characters");
            });
        }
    }
}
