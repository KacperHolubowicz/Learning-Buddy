using FluentValidation;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestionsWithAnswers
{
    public class GetListOfQuestionsWithAnswersValidator :
        AbstractValidator<GetListOfQuestionsWithAnswersQuery>
    {
        public GetListOfQuestionsWithAnswersValidator()
        {
            RuleFor(q => q.QuizID)
                .NotEmpty().WithMessage("Provided quiz ID cannot be empty");
        }
    }
}
