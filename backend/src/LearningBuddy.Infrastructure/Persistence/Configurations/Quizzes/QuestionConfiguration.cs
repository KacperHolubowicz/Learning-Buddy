using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Quizzes
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.ID);
            builder.Property(q => q.Content)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(q => q.Points)
                .IsRequired();
        }
    }
}
