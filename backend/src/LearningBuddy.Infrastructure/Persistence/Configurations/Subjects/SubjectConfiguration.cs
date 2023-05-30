using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Subjects
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.ID);
            builder.Property(s => s.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(s => s.Description)
                .HasMaxLength(200);
            builder.Property(s => s.Finished)
                .HasDefaultValue(false);
            builder.Property(s => s.Public)
                .HasDefaultValue(false);
            builder.HasMany(s => s.Sources)
                .WithOne(sr => sr.Subject);
            builder.HasMany(s => s.Tags)
                .WithMany(t => t.Subjects);
            builder.HasMany(s => s.Tasks)
                .WithOne(t => t.Subject);
            builder.HasOne(s => s.Creator)
                .WithMany(c => c.Subjects);
        }
    }
}
