using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Subjects
{
    public class LearningSourceConfiguration : IEntityTypeConfiguration<LearningSource>
    {
        public void Configure(EntityTypeBuilder<LearningSource> builder)
        {
            builder.HasKey(ls => ls.ID);
            builder.Property(ls => ls.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(ls => ls.Description)
                .HasMaxLength(200);
            builder.Property(ls => ls.Public)
                .HasDefaultValue(false);
            builder.Property(ls => ls.Type)
                .IsRequired();
            builder.HasOne(ls => ls.User)
                .WithMany(u => u.Sources);
            builder.HasOne(ls => ls.Subject)
                .WithMany(s => s.Sources);
        }
    }
}
