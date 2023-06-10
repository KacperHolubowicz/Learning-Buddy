using LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.DeleteSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class DeleteSubjectTaskEndpoint : BaseEndpoint<DeleteSubjectTaskCommand, bool>
    {
        public override void Configure()
        {
            Delete(Url + "task/{SubjectTaskID}");
        }

        public override async Task HandleAsync(DeleteSubjectTaskCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
