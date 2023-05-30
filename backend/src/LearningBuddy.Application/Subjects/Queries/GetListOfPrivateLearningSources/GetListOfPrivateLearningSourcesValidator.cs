using FluentValidation;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfPrivateLearningSources
{
    public class GetListOfPrivateLearningSourcesValidator : 
        AbstractValidator<GetListOfPrivateLearningSourcesQuery>
    {
        public GetListOfPrivateLearningSourcesValidator()
        {
            RuleFor(x => x.SubjectID)
                .NotEmpty().WithMessage("Subject id must be provided");
            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("User id must be provided");
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be equal or greater than 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Page size must be equal or greater than 2");
        }
    }
}
