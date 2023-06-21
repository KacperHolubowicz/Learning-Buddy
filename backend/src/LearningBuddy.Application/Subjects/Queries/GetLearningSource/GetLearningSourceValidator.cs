using FluentValidation;

namespace LearningBuddy.Application.Subjects.Queries.GetLearningSource
{
    public class GetLearningSourceValidator : AbstractValidator<GetLearningSourceQuery>
    {
        public GetLearningSourceValidator()
        {
            RuleFor(x => x.LearningSourceID)
                .NotEmpty()
                .WithMessage("Learning source id must be provided");
        }
    }
}
