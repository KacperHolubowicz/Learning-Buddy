using LearningBuddy.Application.Common.Interfaces.Auth;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Common.Interfaces.Security;
using LearningBuddy.Infrastructure.Auth;
using LearningBuddy.Infrastructure.Persistence;
using LearningBuddy.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string? dbType = configuration["DatabaseType"];
                options.UseNpgsql(configuration.GetConnectionString(dbType == null ? "Testing" : dbType),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<ISubjectsDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IQuizzesDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUsersDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
