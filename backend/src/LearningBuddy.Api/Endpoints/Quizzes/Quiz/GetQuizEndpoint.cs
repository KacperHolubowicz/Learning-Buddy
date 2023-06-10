using LearningBuddy.Application.Quizzes.Queries.GetQuiz;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class GetQuizEndpoint : BaseEndpoint<GetQuizQuery, QuizDTO>
    {
        public override void Configure()
        {
            Get(Url + "quiz/{QuizID}");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetQuizQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
