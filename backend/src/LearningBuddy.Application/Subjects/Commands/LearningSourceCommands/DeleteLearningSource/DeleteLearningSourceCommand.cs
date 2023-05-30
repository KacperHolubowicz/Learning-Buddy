using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.DeleteLearningSource
{
    public record DeleteLearningSourceCommand : ICommand<bool>
    {
        public long UserID { get; set; }
        public long SourceID { get; set; }
    }

    public class DeleteLearningSourceHandler : ICommandHandler<DeleteLearningSourceCommand, bool>
    {
        private readonly ISubjectsDbContext context;

        public DeleteLearningSourceHandler(ISubjectsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteLearningSourceCommand request, CancellationToken cancellationToken)
        {
            LearningSource sourceToDelete = await context.Sources
                .FirstOrDefaultAsync(s => s.ID == request.SourceID
                    && s.User.ID == request.UserID);
            if(sourceToDelete == null)
            {
                throw new ResourceNotFoundException("LearningSource", request.SourceID);
            }
            context.Sources.Remove(sourceToDelete);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
