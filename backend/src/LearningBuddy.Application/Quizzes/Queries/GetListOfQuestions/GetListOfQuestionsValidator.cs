using FluentValidation;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions
{
    public class GetListOfQuestionsValidator : AbstractValidator<GetListOfQuestionsQuery>
    {
        public GetListOfQuestionsValidator()
        {
            RuleFor(q => q.QuizID)
                .NotEmpty().WithMessage("Provided quiz ID cannot be empty");
        }
    }
}
