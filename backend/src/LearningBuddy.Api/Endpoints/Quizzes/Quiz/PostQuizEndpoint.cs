using LearningBuddy.Application.Quizzes.Commands.QuizCommands.CreateQuiz;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class PostQuizEndpoint : BaseEndpoint<CreateQuizCommand, long>
    {
        public override void Configure()
        {
            Post(Url + "quiz");
        }

        public override async Task HandleAsync(CreateQuizCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
