using LearningBuddy.Application.Common;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjectTasks;

namespace LearningBuddy.Api.Endpoints.Subjects.SubjectTask
{
    public class GetSubjectTasksEndpoint 
        : BaseEndpoint<GetListOfSubjectTasksQuery, PaginatedList<SubjectTaskItemDTO>>
    {
        public override void Configure()
        {
            Get(Url + "subject/{SubjectID}/task");
        }

        public override async Task HandleAsync(GetListOfSubjectTasksQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
