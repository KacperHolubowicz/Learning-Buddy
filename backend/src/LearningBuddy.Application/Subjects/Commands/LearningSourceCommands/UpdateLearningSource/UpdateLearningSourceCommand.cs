using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Subjects.Enums;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.UpdateLearningSource
{
    public record UpdateLearningSourceCommand : ICommand<bool>
    {
        public long UserID { get; set; }
        public long SourceID { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public bool? Public { get; init; }
        public SourceType? Type { get; init; }
    }

    public class UpdateLearningSourceHandler : ICommandHandler<UpdateLearningSourceCommand, bool>
    {
        private readonly ISubjectsDbContext context;

        public UpdateLearningSourceHandler(ISubjectsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(UpdateLearningSourceCommand request, CancellationToken cancellationToken)
        {
            LearningSource sourceToEdit = await context.Sources
                .FirstOrDefaultAsync(s => s.ID == request.SourceID
                    && s.User.ID == request.UserID);
            if (sourceToEdit == null)
            {
                throw new ResourceNotFoundException("Subject", request.SourceID);
            }

            UpdateSource(request, sourceToEdit);
            context.Sources.Update(sourceToEdit);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private void UpdateSource(UpdateLearningSourceCommand request, LearningSource sourceToEdit)
        {
            sourceToEdit.Description = request.Description ?? sourceToEdit.Description;
            sourceToEdit.Public = request.Public ?? sourceToEdit.Public;
            sourceToEdit.Name = request.Name ?? sourceToEdit.Name;
            sourceToEdit.Type = request.Type ?? sourceToEdit.Type;
        }
    }
}
