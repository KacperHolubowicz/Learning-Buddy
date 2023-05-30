using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Commands.QuizCommands.DeleteQuiz
{
    public record DeleteQuizCommand : ICommand<bool>
    {
        public long UserID { get; set; }
        public long QuizID { get; set; }
    }

    public class DeleteQuizCommandHandler : ICommandHandler<DeleteQuizCommand, bool>
    {
        private readonly IQuizzesDbContext qContext;

        public DeleteQuizCommandHandler(IQuizzesDbContext qContext)
        {
            this.qContext = qContext;
        }
        public async Task<bool> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz foundQuiz = await FindQuiz(request.UserID, request.QuizID);
            if (foundQuiz != null)
            {
                qContext.Quizzes.Remove(foundQuiz);
                await qContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        private async Task<Quiz> FindQuiz(long userID, long quizID)
        {
            Quiz quiz = await qContext.Quizzes
                .Include(q => q.User)
                .FirstOrDefaultAsync(q => q.ID == quizID && q.User.ID == userID);
            return quiz;
        }
    }
}
