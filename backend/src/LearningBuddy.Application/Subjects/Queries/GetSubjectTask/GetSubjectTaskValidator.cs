using FluentValidation;

namespace LearningBuddy.Application.Subjects.Queries.GetSubjectTask
{
    public class GetSubjectTaskValidator : AbstractValidator<GetSubjectTaskQuery>
    {
        public GetSubjectTaskValidator()
        {
            RuleFor(x => x.SubjectTaskID)
                .NotEmpty()
                .WithMessage("Subject task id must be provided");
        }
    }
}
