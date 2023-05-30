using FluentValidation;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources
{
    public class GetListOfLearningSourcesValidator : AbstractValidator<GetListOfLearningSourcesQuery>
    {
        public GetListOfLearningSourcesValidator()
        {
            RuleFor(x => x.SubjectID)
                .NotEmpty().WithMessage("Subject id must be provided");
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be equal or greater than 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Page size must be equal or greater than 2");
        }
    }
}
