using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

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
                .Include(s => s.Creator)
                .FirstOrDefaultAsync(s => s.ID == command.SubjectID);
            CheckAccessToSubject(command, creator, subject);
            return new SubjectTask()
            {
                Name = command.Name,
                Description = command.Description,
                Finished = false,
                CreatedAt = DateTimeOffset.UtcNow,
                Deadline = command.Deadline.Value,
                Difficulty = command.Difficulty,
                User = creator,
                Priority = command.Priority,
                Subject = subject
            };
        }

        private void CheckAccessToSubject(CreateSubjectTaskCommand req, User user, Subject subject)
        {
            if(subject == null)
            {
                throw new ResourceNotFoundException("Subject", req.SubjectID);
            } 
            else if(user == null)
            {
                throw new ResourceNotFoundException("User", req.UserID);
            }
            else if(!subject.Public && subject.Creator.ID != user.ID)
            {
                throw new UnauthorizedResourceAccessException("Subject", subject.ID);
            }
        }
    }
}
