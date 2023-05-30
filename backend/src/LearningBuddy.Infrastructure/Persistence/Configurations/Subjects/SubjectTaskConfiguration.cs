using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Subjects
{
    public class SubjectTaskConfiguration : IEntityTypeConfiguration<SubjectTask>
    {
        public void Configure(EntityTypeBuilder<SubjectTask> builder)
        {
            builder.HasKey(st => st.ID);
            builder.Property(st => st.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(st => st.Description)
                .HasMaxLength(200);
            builder.Property(st => st.CreatedAt)
                .HasDefaultValue(DateTimeOffset.UtcNow)
                .IsRequired();
            builder.Property(st => st.Priority)
                .IsRequired();
            builder.Property(st => st.Difficulty)
                .IsRequired();
            builder.HasOne(st => st.User)
                .WithMany(u => u.Tasks);
            builder.HasOne(st => st.Subject)
                .WithMany(s => s.Tasks);
               
        }
    }
}
