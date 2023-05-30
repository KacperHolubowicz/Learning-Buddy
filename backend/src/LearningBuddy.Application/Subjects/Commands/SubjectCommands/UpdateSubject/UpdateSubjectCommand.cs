using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.SubjectCommands.UpdateSubject
{
    public record UpdateSubjectCommand : ICommand<bool>
    {
        public long SubjectID { get; set; }
        public long UserID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public bool? Public { get; init; }
        public bool? Finished { get; set; }
        public byte[]? Thumbnail { get; init; }
        public IEnumerable<string>? Tags { get; init; }
    }

    public class UpdateSubjectHandler : ICommandHandler<UpdateSubjectCommand, bool>
    {
        private readonly ISubjectsDbContext context;

        public UpdateSubjectHandler(ISubjectsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject? subjectToEdit = await context.Subjects
                .FirstOrDefaultAsync(s => s.ID == request.SubjectID
                    && s.Creator.ID == request.UserID);
            if (subjectToEdit == null)
            {
                throw new ResourceNotFoundException("Subject", request.SubjectID);
            }
            else if (subjectToEdit.Finished)
            {
                return false;
            }

            await UpdateSubject(request, subjectToEdit);
            context.Subjects.Update(subjectToEdit);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task UpdateSubject(UpdateSubjectCommand request, Subject subjectToEdit)
        {
            subjectToEdit.Description = request.Description ?? subjectToEdit.Description;
            subjectToEdit.Public = request.Public ?? subjectToEdit.Public;
            subjectToEdit.Name = request.Name ?? subjectToEdit.Name;
            subjectToEdit.Tags = request.Tags == null ? subjectToEdit.Tags
                : await FindOrCreateTags(request.Tags);
            subjectToEdit.Thumbnail = request.Thumbnail ?? subjectToEdit.Thumbnail;
            subjectToEdit.Finished = request.Finished ?? subjectToEdit.Finished;
        }

        private async Task<ICollection<Tag>> FindOrCreateTags(IEnumerable<string> tagStrings)
        {
            ICollection<Tag> tags = new List<Tag>(); ;
            foreach (string tagString in tagStrings)
            {
                Tag? existingTag = await context.Tags
                    .FirstOrDefaultAsync(t => t.Value.ToLower() == tagString.ToLower());
                Tag tag = existingTag == null ? new Tag() { Value = tagString.ToLower() } : existingTag;
                tags.Add(tag);
            }
            return tags;
        }
    }
}
