using LearningBuddy.Application.Subjects.Commands.SubjectCommands.UpdateSubject;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class UpdateSubjectEndpoint : BaseEndpoint<UpdateSubjectCommand, bool>
    {
        public override void Configure()
        {
            Put(Url + "subject/{SubjectID}");
        }

        public override async Task HandleAsync(UpdateSubjectCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
