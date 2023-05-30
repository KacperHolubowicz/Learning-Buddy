using FluentValidation;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes
{
    public class GetListOfQuizzesValidator : AbstractValidator<GetListOfQuizzesQuery>
    {
        public GetListOfQuizzesValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be equal or greater than 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Page size must be equal or greater than 2");
            RuleFor(x => x.SubjectID)
                .NotEmpty().WithMessage("Provided subject ID cannot be empty");
        }
    }
}
