using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.DeleteSubjectTask
{
    public record DeleteSubjectTaskCommand : ICommand<bool>
    {
        public long UserID { get; set; }
        public long SubjectTaskID { get; set; }
    }

    public class DeleteSubjectTaskHandler : ICommandHandler<DeleteSubjectTaskCommand, bool>
    {
        private readonly ISubjectsDbContext context;

        public DeleteSubjectTaskHandler(ISubjectsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteSubjectTaskCommand request, CancellationToken cancellationToken)
        {

            SubjectTask subjectTaskToDelete = await context.Tasks
                .FirstOrDefaultAsync(s => s.ID == request.SubjectTaskID
                    && s.User.ID == request.UserID);

            if (subjectTaskToDelete == null)
            {
                return false;
            }
            context.Tasks.Remove(subjectTaskToDelete);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
