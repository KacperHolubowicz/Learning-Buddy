using LearningBuddy.Application.Common;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjects;
using MediatR;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class GetSubjectsEndpoint : BaseEndpoint<GetListOfSubjectsQuery, PaginatedList<SubjectItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject");
            AllowAnonymous();
            ResponseCache(30);
        }

        public override async Task HandleAsync(GetListOfSubjectsQuery req, CancellationToken ct)
        {
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
