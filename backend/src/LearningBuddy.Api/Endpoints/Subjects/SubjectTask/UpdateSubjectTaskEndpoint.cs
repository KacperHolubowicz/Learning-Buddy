using LearningBuddy.Application.Subjects.Commands.SubjectTaskCommands.UpdateSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class UpdateSubjectTaskEndpoint : BaseEndpoint<UpdateSubjectTaskCommand, bool>
    {
        public override void Configure()
        {
            Put(Url + "task/{SubjectTaskID}");
        }

        public override async Task HandleAsync(UpdateSubjectTaskCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
