using LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class GetQuestionsEndpoint : BaseEndpoint<GetListOfQuestionsQuery, IList<QuestionItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "quiz/{QuizID}/question");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetListOfQuestionsQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            if (userId != 0)
            {
                req.UserID = userId;
            }
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
