using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Common.Interfaces.Security;
using LearningBuddy.Domain.Users.Entities;

namespace LearningBuddy.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand : ICommand<bool>
    {
        public string Username { get; init; }
        public string Login { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string RepeatPassword { get; init; }
    }

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IUsersDbContext context;
        private readonly IEncryptionService encryptionService;

        public RegisterUserCommandHandler(IMapper mapper, IUsersDbContext context,
            IEncryptionService encryptionService)
        {
            this.mapper = mapper;
            this.context = context;
            this.encryptionService = encryptionService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await CheckPropertyUniqueness(request.Username, request.Login, request.Email);
            User newUser = mapper.Map<User>(request);
            Tuple<byte[], byte[]> passwordAndSalt = encryptionService.HashPasswordWithNewSalt(request.Password);
            newUser.Password = passwordAndSalt.Item1;
            newUser.Salt = passwordAndSalt.Item2;
            await context.Users.AddAsync(newUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task CheckPropertyUniqueness(string username, string login, string email)
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();
            if(!await context.IsUsernameUnique(username))
            {
                properties.Add("Username", username);
            }
            if(!await context.IsLoginUnique(login))
            {
                properties.Add("Login", login);
            }
            if(!await context.IsEmailUnique(email))
            {
                properties.Add("Email", email);
            }
            if(properties.Count > 0)
            {
                throw new PropertyNotUniqueException(properties);
            }
        }
    }
}
