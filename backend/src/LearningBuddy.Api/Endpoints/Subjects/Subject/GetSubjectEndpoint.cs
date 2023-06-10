using LearningBuddy.Application.Subjects.Queries.GetSubject;
using MediatR;

namespace LearningBuddy.Api.Endpoints.Subjects.Subject
{
    public class GetSubjectEndpoint : BaseEndpoint<GetSubjectQuery, SubjectDTO>
    {
        public override void Configure()
        {
            Get(Url + "subject/{SubjectID}");
            ResponseCache(60);
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetSubjectQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
