using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Quizzes
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasKey(q => q.ID);
            builder.HasOne(q => q.User)
                .WithMany(u => u.Quizzes);
            builder.HasOne(q => q.Subject)
                .WithMany(s => s.Quizzes);
            builder.HasMany(q => q.Questions)
                .WithOne(qu => qu.Quiz);
            builder.HasMany(q => q.Attempts)
                .WithOne(a => a.Quiz);
            builder.Property(q => q.CreatedAt)
                .HasDefaultValue(DateTimeOffset.UtcNow)
                .IsRequired();
            builder.Property(q => q.ModifiedAt)
                .HasDefaultValue(DateTimeOffset.UtcNow)
                .IsRequired();
            builder.Property(q => q.MaxPoints)
                .IsRequired();
            builder.Property(q => q.Description)
                .HasMaxLength(200);
            builder.Property(q => q.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
