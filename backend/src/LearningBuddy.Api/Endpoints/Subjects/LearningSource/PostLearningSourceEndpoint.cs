using LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.CreateLearningSource;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class PostLearningSourceEndpoint : BaseEndpoint<CreateLearningSourceCommand>
    {
        public override void Configure()
        {
            Post(Url + "subject/{SubjectID}/learning-source");
        }

        public override async Task HandleAsync(CreateLearningSourceCommand req, CancellationToken ct)
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
