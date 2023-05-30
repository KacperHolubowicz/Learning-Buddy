using FluentValidation;

namespace LearningBuddy.Application.Quizzes.Commands.QuizCommands.DeleteQuiz
{
    public class DeleteQuizValidator : AbstractValidator<DeleteQuizCommand>
    {
        public DeleteQuizValidator()
        {
            RuleFor(x => x.QuizID)
                .NotEmpty()
                .WithMessage("Quiz ID must be provided");
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User ID must be provided");
        }
    }
}
