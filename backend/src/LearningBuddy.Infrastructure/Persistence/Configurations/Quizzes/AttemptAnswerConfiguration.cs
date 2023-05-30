using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Quizzes
{
    public class AttemptAnswerConfiguration : IEntityTypeConfiguration<AttemptAnswer>
    {
        public void Configure(EntityTypeBuilder<AttemptAnswer> builder)
        {
            builder.HasKey(aa => aa.ID);
            builder.HasOne(aa => aa.Question)
                .WithMany();
            builder.HasOne(aa => aa.Answer)
                .WithMany();
            builder.HasOne(aa => aa.Attempt)
                .WithMany();
        }
    }
}
