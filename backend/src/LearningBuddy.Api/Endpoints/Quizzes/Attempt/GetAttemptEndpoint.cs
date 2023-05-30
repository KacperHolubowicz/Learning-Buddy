using LearningBuddy.Application.Quizzes.Queries.GetAttempt;

namespace LearningBuddy.Api.Endpoints.Quizzes.Attempt
{
    public class GetAttemptEndpoint : BaseEndpoint<GetAttemptQuery, AttemptDTO>
    {
        public override void Configure()
        {
            Get(Url + "attempt/{AttemptID}");
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetAttemptQuery req, CancellationToken ct)
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
