using LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.CreateSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class PostSubjectTaskEndpoint : BaseEndpoint<CreateSubjectTaskCommand>
    {
        public override void Configure()
        {
            Post(Url + "subject/{SubjectID}/task");
        }

        public override async Task HandleAsync(CreateSubjectTaskCommand req, CancellationToken ct)
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
