using LearningBuddy.Application.Quizzes.Commands.AttemptCommands.CreateAttempt;

namespace LearningBuddy.Api.Endpoints.Quizzes.Attempt
{
    public class CreateAttemptEndpoint : BaseEndpoint<CreateAttemptCommand, AttemptCreateResponseDTO>
    {
        public override void Configure()
        {
            Post(Url + "quiz/attempt");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateAttemptCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
