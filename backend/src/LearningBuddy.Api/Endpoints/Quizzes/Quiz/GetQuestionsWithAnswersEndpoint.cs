using LearningBuddy.Application.Quizzes.Queries.GetListOfQuestionsWithAnswers;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class GetQuestionsWithAnswersEndpoint : 
        BaseEndpoint<GetListOfQuestionsWithAnswersQuery, IList<QuestionWithAnswerItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "quiz/{QuizID}/question/answers");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetListOfQuestionsWithAnswersQuery req,
            CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}