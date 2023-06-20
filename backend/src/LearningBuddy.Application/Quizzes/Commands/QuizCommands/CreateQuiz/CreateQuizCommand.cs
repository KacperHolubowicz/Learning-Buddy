using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Commands.QuizCommands.CreateQuiz
{
    public record CreateQuizCommand : ICommand<long>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public long UserID { get; set; }
        public long SubjectID { get; set; }
        public ICollection<QuestionCommand> Questions { get; set; }
    }

    public record QuestionCommand
    {
        public string Content { get; set; }
        public byte Points { get; set; }
        public ICollection<AnswerCommand> Answers { get; set; }
    }

    public record AnswerCommand
    {
        public string Content { get; set; }
        public bool Correct { get; set; }
    }

    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, long>
    {
        private readonly IQuizzesDbContext qContext;
        private readonly IUsersDbContext uContext;
        private readonly ISubjectsDbContext sContext;

        public CreateQuizCommandHandler(IQuizzesDbContext qContext, 
            IUsersDbContext uContext, ISubjectsDbContext sContext)
        {
            this.qContext = qContext;
            this.uContext = uContext;
            this.sContext = sContext;
        }

        public async Task<long> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz newQuiz = await CommandToEntity(request);
            await qContext.Quizzes
                .AddAsync(newQuiz);
            await qContext.SaveChangesAsync(cancellationToken);
            return newQuiz.ID;
        }

        private Answer CommandToEntity(AnswerCommand answer)
        {
            return new Answer
            {
                Content = answer.Content,
                Correct = answer.Correct
            };
        }

        private Question CommandToEntity(QuestionCommand question)
        {
            return new Question
            {
                Content = question.Content,
                Points = question.Points,
                Answers = question.Answers.Select(CommandToEntity).ToList()
            };
        }

        private async Task<Quiz> CommandToEntity(CreateQuizCommand quiz)
        {
            Subject sub = await sContext.Subjects
                .Include(s => s.Creator)
                .FirstOrDefaultAsync(s => s.ID == quiz.SubjectID);
            if(sub == null)
            {
                throw new ResourceNotFoundException("Subject", quiz.SubjectID);
            } else if(!sub.Public && sub.Creator.ID != quiz.UserID)
            {
                throw new UnauthorizedResourceAccessException("Subject", quiz.SubjectID);
            }

            User user = await uContext.Users
                .FindAsync(quiz.UserID);
            if(user == null)
            {
                throw new ResourceNotFoundException("User", quiz.UserID);
            }

            return new Quiz
            {
                Description = quiz.Description,
                MaxPoints = (short)quiz.Questions.Sum(qu => qu.Points),
                Name = quiz.Name,
                Questions = quiz.Questions.Select(CommandToEntity).ToList(),
                Subject = sub,
                User = user,
                CreatedAt = DateTimeOffset.UtcNow,
                ModifiedAt = DateTimeOffset.UtcNow
            };
        }
    }
}
