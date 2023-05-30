using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Quizzes
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.ID);
            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers);
            builder.Property(a => a.Content)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(a => a.Correct)
                .IsRequired();
        }
    }
}
