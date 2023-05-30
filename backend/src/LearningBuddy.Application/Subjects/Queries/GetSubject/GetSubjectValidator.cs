using FluentValidation;

namespace LearningBuddy.Application.Subjects.Queries.GetSubject
{
    public class GetSubjectValidator : AbstractValidator<GetSubjectQuery>
    {
        public GetSubjectValidator()
        {
            RuleFor(x => x.SubjectID)
                .NotEmpty().WithMessage("Provided subject ID cannot be empty");
        }
    }
}
