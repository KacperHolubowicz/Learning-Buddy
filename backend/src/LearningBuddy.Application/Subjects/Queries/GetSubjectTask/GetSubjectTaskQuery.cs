using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjectTasks;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetSubjectTask
{
    public record GetSubjectTaskQuery : IQuery<SubjectTaskItemDTO>
    {
        public long UserID { get; set; }
        public long SubjectTaskID { get; set; }
    }

    public class GetSubjectTaskQueryHandler : IQueryHandler<GetSubjectTaskQuery, SubjectTaskItemDTO>
    {
        private readonly ISubjectsDbContext sContext;
        private readonly IMapper mapper;

        public GetSubjectTaskQueryHandler(ISubjectsDbContext sContext, IMapper mapper)
        {
            this.sContext = sContext;
            this.mapper = mapper;
        }

        public async Task<SubjectTaskItemDTO> Handle(GetSubjectTaskQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<SubjectTaskItemDTO>
                (await FindSubjectTask(request.UserID, request.SubjectTaskID));
        }

        public async Task<SubjectTask> FindSubjectTask(long userId, long subjectTaskId)
        {
            SubjectTask task = await sContext.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.ID == subjectTaskId);

            if(task == null)
            {
                throw new ResourceNotFoundException("SubjectTask", subjectTaskId);
            } else if(task.User.ID != userId)
            {
                throw new UnauthorizedResourceAccessException("SubjectTask", subjectTaskId);
            }
            return task;
        }
    }
}
