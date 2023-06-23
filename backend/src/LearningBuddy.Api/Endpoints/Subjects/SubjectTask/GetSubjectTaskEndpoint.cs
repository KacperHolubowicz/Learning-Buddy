using LearningBuddy.Application.Subjects.Queries.GetListOfSubjectTasks;
using LearningBuddy.Application.Subjects.Queries.GetSubjectTask;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class GetSubjectTaskEndpoint : BaseEndpoint<GetSubjectTaskQuery, SubjectTaskItemDTO>
    {
        public override void Configure()
        {
            Get(Url + "task/{SubjectTaskID}");
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetSubjectTaskQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
