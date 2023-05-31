using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningBuddy.Infrastructure.Persistence.Configurations.Users
{
    public class FavouriteSubjectConfiguration : IEntityTypeConfiguration<FavouriteSubject>
    {
        public void Configure(EntityTypeBuilder<FavouriteSubject> builder)
        {
            builder.HasKey(fs => fs.ID);
            builder.HasOne(fs => fs.User);
            builder.HasOne(fs => fs.Subject);
        }
    }
}
