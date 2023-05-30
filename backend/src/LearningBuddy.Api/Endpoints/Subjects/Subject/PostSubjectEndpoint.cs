using LearningBuddy.Application.Subjects.Commands.SubjectCommands.CreateSubject;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class PostSubjectEndpoint : BaseEndpoint<CreateSubjectCommand>
    {
        public override void Configure()
        {
            Post(Url + "subject");
        }

        public override async Task HandleAsync(CreateSubjectCommand req, CancellationToken ct)
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
