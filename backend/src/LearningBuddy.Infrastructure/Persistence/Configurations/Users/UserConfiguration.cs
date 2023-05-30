using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.ID);
            builder.HasMany(u => u.Subjects)
                .WithOne(s => s.Creator);
            builder.HasMany(u => u.Sources)
                .WithOne(s => s.User);
            builder.HasMany(u => u.Tasks)
                .WithOne(t => t.User);
            builder.HasMany(u => u.Attempts)
                .WithOne(a => a.User);
            builder.HasMany(u => u.Quizzes)
                .WithOne(q => q.User);
            builder.Property(u => u.Description)
                .HasMaxLength(200);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(60);
            builder.HasAlternateKey(u => u.Email);
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);
            builder.HasAlternateKey(u => u.Username);
            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(30);
            builder.HasIndex(u => u.Login)
                .IsUnique();
            builder.Property(u => u.Password)
                .IsRequired();
            builder.Property(u => u.Salt)
                .IsRequired();
        }
    }
}
