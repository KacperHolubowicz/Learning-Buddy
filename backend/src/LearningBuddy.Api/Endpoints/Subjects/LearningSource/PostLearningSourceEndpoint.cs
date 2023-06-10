using LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.CreateLearningSource;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class PostLearningSourceEndpoint : BaseEndpoint<CreateLearningSourceCommand, long>
    {
        public override void Configure()
        {
            Post(Url + "subject/{SubjectID}/learning-source");
        }

        public override async Task HandleAsync(CreateLearningSourceCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
