using LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.UpdateSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class UpdateSubjectTaskEndpoint : BaseEndpoint<UpdateSubjectTaskCommand>
    {
        public override void Configure()
        {
            Put(Url + "task/{SubjectTaskID}");
        }

        public override async Task HandleAsync(UpdateSubjectTaskCommand req, CancellationToken ct)
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
