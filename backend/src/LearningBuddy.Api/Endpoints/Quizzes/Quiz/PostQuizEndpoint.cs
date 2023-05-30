using LearningBuddy.Application.Quizzes.Commands.QuizCommands.CreateQuiz;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class PostQuizEndpoint : BaseEndpoint<CreateQuizCommand>
    {
        public override void Configure()
        {
            Post(Url + "quiz");
        }

        public override async Task HandleAsync(CreateQuizCommand req, CancellationToken ct)
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
