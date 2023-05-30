using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.SubjectCommands.CreateSubject
{
    public record CreateSubjectCommand : ICommand<long>
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public bool Public { get; init; }
        public long UserID { get; set; }
        public byte[]? Thumbnail { get; init; }
        public IEnumerable<string> Tags { get; init; }
    }

    public class CreateSubjectHandler : ICommandHandler<CreateSubjectCommand, long>
    {
        private readonly ISubjectsDbContext subjectContext;
        private readonly IUsersDbContext userContext;

        public CreateSubjectHandler(ISubjectsDbContext subjectContext, IUsersDbContext userContext)
        {
            this.subjectContext = subjectContext;
            this.userContext = userContext;
        }

        public async Task<long> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject entity = await CommandToEntity(request);
            await subjectContext.Subjects.AddAsync(entity);
            await subjectContext.SaveChangesAsync(cancellationToken);
            return entity.ID;
        }

        private async Task<Subject> CommandToEntity(CreateSubjectCommand command)
        {
            User creator = await userContext.Users
                .FindAsync(command.UserID);

            if (creator == null)
            {
                throw new ResourceNotFoundException("User", command.UserID);
            }

            ICollection<Tag> tags = await FindOrCreateTags(command.Tags);

            return new Subject()
            {
                Name = command.Name,
                Description = command.Description,
                Finished = false,
                Public = command.Public,
                Thumbnail = command.Thumbnail,
                Creator = creator,
                Tags = tags
            };
        }

        private async Task<ICollection<Tag>> FindOrCreateTags(IEnumerable<string> tagStrings)
        {
            ICollection<Tag> tags = new List<Tag>(); ;
            foreach(string tagString in tagStrings)
            {
                Tag existingTag = await subjectContext.Tags
                    .FirstOrDefaultAsync(t => t.Value.ToLower() == tagString.ToLower());
                Tag tag = existingTag == null ? new Tag() { Value = tagString.ToLower() } : existingTag;
                tags.Add(tag);
            }
            return tags;
        }
    }
}
