using LearningBuddy.Application.Common;
using LearningBuddy.Application.Quizzes.Queries.GetListOfAttempts;

namespace LearningBuddy.Api.Endpoints.Quizzes.Attempt
{
    public class GetAttemptsEndpoint : BaseEndpoint<GetListOfAttemptsQuery, PaginatedList<AttemptItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "quiz/{QuizID}/attempt");
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetListOfAttemptsQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
