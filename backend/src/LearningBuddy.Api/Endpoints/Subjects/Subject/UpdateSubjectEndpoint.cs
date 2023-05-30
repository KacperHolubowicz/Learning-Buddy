using LearningBuddy.Application.Subjects.Commands.SubjectCommands.UpdateSubject;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class UpdateSubjectEndpoint : BaseEndpoint<UpdateSubjectCommand>
    {
        public override void Configure()
        {
            Put(Url + "subject/{SubjectID}");
        }

        public override async Task HandleAsync(UpdateSubjectCommand req, CancellationToken ct)
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
