using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;

namespace LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.CreateSubjectTask
{
    public record CreateSubjectTaskCommand : ICommand<long>
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public int Priority { get; init; }
        public int Difficulty { get; init; }
        public DateTimeOffset? Deadline { get; init; }
        public long UserID { get; set; }
        public long SubjectID { get; set; }
    }

    public class CreateSubjectTaskHandler : ICommandHandler<CreateSubjectTaskCommand, long>
    {
        private readonly IUsersDbContext userContext;
        private readonly ISubjectsDbContext subjectContext;

        public CreateSubjectTaskHandler(IUsersDbContext userContext, ISubjectsDbContext subjectContext)
        {
            this.userContext = userContext;
            this.subjectContext = subjectContext;
        }

        public async Task<long> Handle(CreateSubjectTaskCommand request, CancellationToken cancellationToken)
        {
            SubjectTask entity = await CommandToEntity(request);
            await subjectContext.Tasks.AddAsync(entity);
            await subjectContext.SaveChangesAsync(cancellationToken);
            return entity.ID;
        }

        private async Task<SubjectTask> CommandToEntity(CreateSubjectTaskCommand command)
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

            return new SubjectTask()
            {
                Name = command.Name,
                Description = command.Description,
                Finished = false,
                CreatedAt = DateTime.UtcNow,
                Deadline = command.Deadline.Value,
                Difficulty = command.Difficulty,
                User = creator,
                Priority = command.Priority,
                Subject = subject
            };
        }
    }
}
