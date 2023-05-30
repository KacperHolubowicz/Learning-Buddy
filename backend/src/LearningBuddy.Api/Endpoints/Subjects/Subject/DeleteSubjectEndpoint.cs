using LearningBuddy.Application.Subjects.Commands.SubjectCommands.DeleteSubject;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class DeleteSubjectEndpoint : BaseEndpoint<DeleteSubjectCommand>
    {
        public override void Configure()
        {
            Delete(Url + "subject/{SubjectID}");
        }

        public override async Task HandleAsync(DeleteSubjectCommand req, CancellationToken ct)
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
