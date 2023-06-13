using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Quizzes
{
    public class AttemptConfiguration : IEntityTypeConfiguration<Attempt>
    {
        public void Configure(EntityTypeBuilder<Attempt> builder)
        {
            builder.HasKey(a => a.ID);
            builder.HasOne(a => a.User)
                .WithMany(u => u.Attempts);
            builder.HasOne(a => a.Quiz)
                .WithMany(q => q.Attempts);
            builder.HasMany(a => a.Answers)
                .WithOne(an => an.Attempt);
            builder.Property(a => a.AttemptedAt)
                .IsRequired();
            builder.Property(a => a.Points)
                .IsRequired();
            builder.Property(a => a.MaxPoints)
                .IsRequired();
        }
    }
}
