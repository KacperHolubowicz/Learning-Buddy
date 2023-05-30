using LearningBuddy.Application.Common;
using LearningBuddy.Application.Subjects.Queries.GetListOfPrivateSubjects;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class GetPrivateSubjectsEndpoint : BaseEndpoint<GetListOfPrivateSubjectsQuery, PaginatedList<PrivateSubjectItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject/private");
        }

        public override async Task HandleAsync(GetListOfPrivateSubjectsQuery req, CancellationToken ct)
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
