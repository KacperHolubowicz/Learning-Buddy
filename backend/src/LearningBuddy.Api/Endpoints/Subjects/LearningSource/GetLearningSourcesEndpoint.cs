using LearningBuddy.Application.Common;
using LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources;

namespace LearningBuddy.Api.Endpoints.Subjects.LearningSource
{
    public class GetLearningSourcesEndpoint 
        : BaseEndpoint<GetListOfLearningSourcesQuery, PaginatedList<LearningSourceDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject/{SubjectID}/learning-source");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetListOfLearningSourcesQuery req, CancellationToken ct)
        {
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
