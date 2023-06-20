using LearningBuddy.Application.Subjects.Queries.GetLearningSource;
using LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class GetLearningSourceEndpoint : BaseEndpoint<GetLearningSourceQuery, LearningSourceDTO>
    {
        public override void Configure()
        {
            Get(Url + "learning-source/{LearningSourceID}");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetLearningSourceQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
