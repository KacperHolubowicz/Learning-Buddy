using FluentValidation;

namespace LearningBuddy.Application.Quizzes.Queries.GetQuiz
{
    public class GetQuizValidator : AbstractValidator<GetQuizQuery>
    {
        public GetQuizValidator()
        {
            RuleFor(q => q.QuizID)
                .NotEmpty().WithMessage("Provided quiz ID cannot be empty");
        }
    }
}
