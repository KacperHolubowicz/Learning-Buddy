using FluentValidation;

namespace LearningBuddy.Application.Quizzes.Commands.QuizCommands.CreateQuiz
{
    public class CreateQuizValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .WithMessage("Name field is required")
                .MinimumLength(3)
                .WithMessage("Name should consist of at least 2 characters")
                .MaximumLength(50)
                .WithMessage("Name should consist of at most 50 characters");
            RuleFor(q => q.Description)
                .MaximumLength(200)
                .WithMessage("Description should consist of at most 200 characters");
            RuleFor(q => q.SubjectID)
                .NotEmpty()
                .WithMessage("Subject ID is required");
            RuleFor(q => q.UserID)
                .NotEmpty()
                .WithMessage("User ID is required");
            RuleForEach(q => q.Questions)
                .SetValidator(new QuestionValidator());
        }

        private class QuestionValidator : AbstractValidator<QuestionCommand>
        {
            public QuestionValidator()
            {
                RuleFor(q => q.Content)
                    .NotEmpty()
                    .WithMessage("Content field is required")
                    .MinimumLength(3)
                    .WithMessage("Content should consist of at least 3 characters")
                    .MaximumLength(100)
                    .WithMessage("Content should consist of at most 100 characters");
                RuleFor(q => q.Points)
                    .NotEmpty()
                    .WithMessage("Points field is required");
                RuleForEach(q => q.Answers)
                    .SetValidator(new AnswerValidator());
            }

            private class AnswerValidator : AbstractValidator<AnswerCommand>
            {
                public AnswerValidator()
                {
                    RuleFor(a => a.Content)
                     .NotEmpty()
                     .WithMessage("Content field is required")
                     .MaximumLength(70)
                     .WithMessage("Content should consist of at most 70 characters");
                }
            }
        }
    }
}
