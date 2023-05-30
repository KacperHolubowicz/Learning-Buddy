using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Common.Interfaces.Persistence
{
    public interface ISubjectsDbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectTask> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LearningSource> Sources { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
