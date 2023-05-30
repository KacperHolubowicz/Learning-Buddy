using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Subjects
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Value)
                .HasMaxLength(30)
                .IsRequired();
            builder.HasMany(t => t.Subjects)
                .WithMany(s => s.Tags);
        }
    }
}
