using LearningBuddy.Application.Common;
using LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes;

namespace LearningBuddy.Api.Endpoints.Quizzes.Quiz
{
    public class GetQuizzesEndpoint : BaseEndpoint<GetListOfQuizzesQuery, PaginatedList<QuizItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject/{SubjectID}/quiz");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetListOfQuizzesQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
