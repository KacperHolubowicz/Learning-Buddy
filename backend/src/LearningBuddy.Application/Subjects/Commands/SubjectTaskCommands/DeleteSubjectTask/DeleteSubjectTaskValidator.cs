using FluentValidation;

namespace LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.DeleteSubjectTask
{
    public class DeleteSubjectTaskValidator : AbstractValidator<DeleteSubjectTaskCommand>
    {
        public DeleteSubjectTaskValidator()
        {
            RuleFor(x => x.SubjectTaskID)
                .NotEmpty()
                .WithMessage("Subject task ID must be provided");
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
        }
    }
}
