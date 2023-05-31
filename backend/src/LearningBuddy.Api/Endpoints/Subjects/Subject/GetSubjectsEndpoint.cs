using LearningBuddy.Application.Common;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjects;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class GetSubjectsEndpoint : BaseEndpoint<GetListOfSubjectsQuery, PaginatedList<SubjectItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject");
            AllowAnonymous();
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetListOfSubjectsQuery req, CancellationToken ct)
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
