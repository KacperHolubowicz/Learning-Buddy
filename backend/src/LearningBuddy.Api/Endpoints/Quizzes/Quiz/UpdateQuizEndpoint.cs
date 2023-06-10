using LearningBuddy.Application.Quizzes.Commands.QuizCommands.UpdateQuiz;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class UpdateQuizEndpoint : BaseEndpoint<UpdateQuizCommand, bool>
    {
        public override void Configure()
        {
            Put(Url + "quiz/{QuizID}");
        }

        public override async Task HandleAsync(UpdateQuizCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
