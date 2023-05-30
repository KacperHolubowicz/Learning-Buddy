using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Common.Interfaces.Persistence
{
    public interface IUsersDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public Task<bool> IsUsernameUnique(string username);

        public Task<bool> IsLoginUnique(string login);

        public Task<bool> IsEmailUnique(string email);
    }
}
