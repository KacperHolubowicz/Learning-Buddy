using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.SubjectCommands.CreateSubject
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("Creator id must be provided");
            RuleFor(x => x.Public)
                .NotNull()
                .WithMessage("Must explicitly provide value for 'Public'");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name value must be provided")
                .Length(3, 30)
                .WithMessage("Name must have between 3 and 30 characters");
            When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(200)
                    .WithMessage("Description must not be longer than 200 characters");
            });
            RuleFor(x => x.Tags)
                .NotEmpty()
                .WithMessage("Must provide at least one tag");
            RuleForEach(x => x.Tags)
                .Length(2, 30)
                .WithMessage("Tag must have between 2 and 30 characters");
        }
    }
}
