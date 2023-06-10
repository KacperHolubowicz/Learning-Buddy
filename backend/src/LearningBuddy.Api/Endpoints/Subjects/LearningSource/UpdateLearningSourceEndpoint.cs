using LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.UpdateLearningSource;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class UpdateLearningSourceEndpoint : BaseEndpoint<UpdateLearningSourceCommand, bool>
    {
        public override void Configure()
        {
            Put(Url + "learning-source/{SourceID}");
        }

        public override async Task HandleAsync(UpdateLearningSourceCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
