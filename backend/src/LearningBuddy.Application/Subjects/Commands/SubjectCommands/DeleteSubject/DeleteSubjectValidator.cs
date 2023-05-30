using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.SubjectCommands.DeleteSubject
{
    public class DeleteSubjectValidator : AbstractValidator<DeleteSubjectCommand>
    {
        public DeleteSubjectValidator()
        {
            RuleFor(x => x.SubjectID)
                .NotEmpty()
                .WithMessage("Subject ID must be provided");
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
        }
    }
}
