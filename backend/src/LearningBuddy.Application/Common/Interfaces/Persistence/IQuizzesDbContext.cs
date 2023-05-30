using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Common.Interfaces.Persistence
{
    public interface IQuizzesDbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<AttemptAnswer> AttemptAnswers { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
