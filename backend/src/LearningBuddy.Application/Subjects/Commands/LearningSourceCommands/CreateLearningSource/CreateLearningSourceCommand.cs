using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Subjects.Enums;
using LearningBuddy.Domain.Users.Entities;

namespace LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.CreateLearningSource
{
    public record CreateLearningSourceCommand : ICommand<long>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Public { get; set; }
        public SourceType Type { get; set; }
        public long UserID { get; set; }
        public long SubjectID { get; set; }
    }

    public class CreateLearningSourceHandler : ICommandHandler<CreateLearningSourceCommand, long>
    {
        private readonly IUsersDbContext userContext;
        private readonly ISubjectsDbContext subjectContext;

        public CreateLearningSourceHandler(IUsersDbContext userContext, ISubjectsDbContext subjectContext)
        {
            this.userContext = userContext;
            this.subjectContext = subjectContext;
        }

        public async Task<long> Handle(CreateLearningSourceCommand request, CancellationToken cancellationToken)
        {
            LearningSource entityToAdd = await CommandToEntity(request);
            await subjectContext.Sources.AddAsync(entityToAdd);
            await subjectContext.SaveChangesAsync(cancellationToken);
            return entityToAdd.ID;
        }

        private async Task<LearningSource> CommandToEntity(CreateLearningSourceCommand command)
        {
            User creator = await userContext.Users
                .FindAsync(command.UserID);
            Subject subject = await subjectContext.Subjects
                .FindAsync(command.SubjectID);

            if (creator == null)
            {
                throw new ResourceNotFoundException("User", command.UserID);
            } else if(subject == null)
            {
                throw new ResourceNotFoundException("Subject", command.SubjectID);
            }

            return new LearningSource()
            {
                Description = command.Description,
                Name = command.Name,
                Public = command.Public,
                Subject = subject,
                User = creator,
                Type = command.Type
            };
        }
    }
}
