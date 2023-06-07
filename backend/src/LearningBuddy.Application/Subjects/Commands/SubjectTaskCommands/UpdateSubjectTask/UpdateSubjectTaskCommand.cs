using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.UpdateSubjectTask
{
    public record UpdateSubjectTaskCommand : ICommand<bool>
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? Priority { get; init; }
        public bool? Finished { get; set; }
        public int? Difficulty { get; init; }
        public DateTimeOffset? Deadline { get; init; }
        public long UserID { get; set; }
        public long SubjectTaskID { get; init; }
    }

    public class UpdateSubjectTaskHandler : ICommandHandler<UpdateSubjectTaskCommand, bool>
    {
        private readonly ISubjectsDbContext context;

        public UpdateSubjectTaskHandler(ISubjectsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(UpdateSubjectTaskCommand request, CancellationToken cancellationToken)
        {

            SubjectTask subjectTaskToEdit = await context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(s => s.ID == request.SubjectTaskID);

            if (subjectTaskToEdit == null)
            {
                throw new ResourceNotFoundException("SubjectTask", request.SubjectTaskID);
            } 
            else if(subjectTaskToEdit.User.ID != request.UserID)
            {
                throw new UnauthorizedResourceAccessException("SubjecTask", request.SubjectTaskID);
            }
            else if (subjectTaskToEdit.Finished)
            {
                return false;
            }

            UpdateSubject(request, subjectTaskToEdit);
            context.Tasks.Update(subjectTaskToEdit);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private void UpdateSubject(UpdateSubjectTaskCommand request, SubjectTask subjectTaskToEdit)
        {
            subjectTaskToEdit.Description = request.Description ?? subjectTaskToEdit.Description;
            subjectTaskToEdit.Name = request.Name ?? subjectTaskToEdit.Name;
            subjectTaskToEdit.Difficulty = request.Difficulty ?? subjectTaskToEdit.Difficulty;
            subjectTaskToEdit.Priority = request.Priority ?? subjectTaskToEdit.Priority;
            subjectTaskToEdit.Deadline = request.Deadline ?? subjectTaskToEdit.Deadline;
            subjectTaskToEdit.Finished = request.Finished ?? subjectTaskToEdit.Finished;
        }
    }
}
