using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Users
{
    public class FavouriteQuizConfiguration : IEntityTypeConfiguration<FavouriteQuiz>
    {
        public void Configure(EntityTypeBuilder<FavouriteQuiz> builder)
        {
            builder.HasKey(fq => fq.ID);
            builder.HasOne(fq => fq.User);
            builder.HasOne(fq => fq.Quiz);
        }
    }
}