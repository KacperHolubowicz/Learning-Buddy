using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.SubjectCommands.DeleteSubject
{
    public record DeleteSubjectCommand : ICommand<bool>
    {
        public long UserID { get; set; }
        public long SubjectID { get; set; }
    }

    public class DeleteSubjectHandler : ICommandHandler<DeleteSubjectCommand, bool>
    {
        private readonly ISubjectsDbContext context;

        public DeleteSubjectHandler(ISubjectsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject subjectToDelete = await context.Subjects
                .FirstOrDefaultAsync(s => s.ID == request.SubjectID
                    && s.Creator.ID == request.UserID);

            if (subjectToDelete == null)
            {
                throw new ResourceNotFoundException("Subject", request.SubjectID);
            }
            context.Subjects.Remove(subjectToDelete);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
