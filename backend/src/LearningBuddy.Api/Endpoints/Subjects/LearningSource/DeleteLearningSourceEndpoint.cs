using LearningBuddy.Application.Subjects.Commands.LearningSourceCommands.DeleteLearningSource;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class DeleteLearningSourceEndpoint : BaseEndpoint<DeleteLearningSourceCommand, bool>
    {
        public override void Configure()
        {
            Delete(Url + "learning-source/{SourceID}");
        }

        public override async Task HandleAsync(DeleteLearningSourceCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
