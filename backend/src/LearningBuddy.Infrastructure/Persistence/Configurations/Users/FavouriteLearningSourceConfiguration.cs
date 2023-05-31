using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Users
{
    public class FavouriteLearningSourceConfiguration : IEntityTypeConfiguration<FavouriteLearningSource>
    {
        public void Configure(EntityTypeBuilder<FavouriteLearningSource> builder)
        {
            builder.HasKey(fl => fl.ID);
            builder.HasOne(fl => fl.User);
            builder.HasOne(fl => fl.LearningSource);
        }
    }
}