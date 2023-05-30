using LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.UpdateLearningSource;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class UpdateLearningSourceEndpoint : BaseEndpoint<UpdateLearningSourceCommand>
    {
        public override void Configure()
        {
            Put(Url + "learning-source/{SourceID}");
        }

        public override async Task HandleAsync(UpdateLearningSourceCommand req, CancellationToken ct)
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
