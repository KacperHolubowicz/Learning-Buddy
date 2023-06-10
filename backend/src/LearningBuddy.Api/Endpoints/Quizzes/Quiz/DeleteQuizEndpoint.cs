using LearningBuddy.Application.Quizzes.Commands.QuizCommands.DeleteQuiz;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class DeleteQuizEndpoint : BaseEndpoint<DeleteQuizCommand, bool>
    {
        public override void Configure()
        {
            Delete(Url + "quiz/{QuizID}");
        }

        public override async Task HandleAsync(DeleteQuizCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
