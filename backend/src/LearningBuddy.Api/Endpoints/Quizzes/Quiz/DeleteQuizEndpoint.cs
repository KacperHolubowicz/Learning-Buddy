using LearningBuddy.Application.Quizzes.Commands.QuizCommands.DeleteQuiz;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class DeleteQuizEndpoint : BaseEndpoint<DeleteQuizCommand>
    {
        public override void Configure()
        {
            Delete(Url + "quiz/{QuizID}");
        }

        public override async Task HandleAsync(DeleteQuizCommand req, CancellationToken ct)
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
