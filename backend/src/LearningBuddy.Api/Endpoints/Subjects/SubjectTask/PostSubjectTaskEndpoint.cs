using LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.CreateSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class PostSubjectTaskEndpoint : BaseEndpoint<CreateSubjectTaskCommand, long>
    {
        public override void Configure()
        {
            Post(Url + "subject/{SubjectID}/task");
        }

        public override async Task HandleAsync(CreateSubjectTaskCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
