using FluentValidation;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfSubjects
{
    public class GetListOfSubjectsValidator : AbstractValidator<GetListOfSubjectsQuery>
    {
        public GetListOfSubjectsValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be equal or greater than 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Page size must be equal or greater than 2");
        }
    }
}
