using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningBuddy.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, ISubjectsDbContext, IUsersDbContext, IQuizzesDbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<AttemptAnswer> AttemptAnswers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectTask> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LearningSource> Sources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<FavouriteSubject> FavouriteSubjects { get; set; }
        public DbSet<FavouriteQuiz> FavouriteQuizzes { get; set; }
        public DbSet<FavouriteLearningSource> FavouriteLearningSources { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsUsernameUnique(string username)
        {
            return await Users.FirstOrDefaultAsync(x => x.Username == username) == null;
        }

        public async Task<bool> IsLoginUnique(string login)
        {
            return await Users.FirstOrDefaultAsync(x => x.Login == login) == null;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return await Users.FirstOrDefaultAsync(x => x.Email == email) == null;
        }
    }
}
