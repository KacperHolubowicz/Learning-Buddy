using LearningBuddy.Application.Common;
using LearningBuddy.Application.Subjects.Queries.GetListOfPrivateLearningSources;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class GetPrivateLearningSourcesEndpoint 
        : BaseEndpoint<GetListOfPrivateLearningSourcesQuery, PaginatedList<PrivateLearningSourceDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject/{SubjectID}/learning-source/private");
        }

        public override async Task HandleAsync(GetListOfPrivateLearningSourcesQuery req, CancellationToken ct)
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
