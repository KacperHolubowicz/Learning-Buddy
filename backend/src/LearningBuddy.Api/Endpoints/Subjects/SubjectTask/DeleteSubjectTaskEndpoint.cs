using LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.DeleteSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class DeleteSubjectTaskEndpoint : BaseEndpoint<DeleteSubjectTaskCommand>
    {
        public override void Configure()
        {
            Delete(Url + "task/{SubjectTaskID}");
        }

        public override async Task HandleAsync(DeleteSubjectTaskCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            if (userId == 0)
            {
                await SendUnauthorizedAsync(ct);
            }
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
