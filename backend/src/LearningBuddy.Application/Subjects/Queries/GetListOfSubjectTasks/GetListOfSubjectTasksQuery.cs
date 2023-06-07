using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfSubjectTasks
{
    public record GetListOfSubjectTasksQuery : IQuery<PaginatedList<SubjectTaskItemDTO>>
    {
        public long UserID { get; set; }
        public long SubjectID { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetListOfSubjectTasksHandler : IQueryHandler<GetListOfSubjectTasksQuery, 
        PaginatedList<SubjectTaskItemDTO>>
    {
        private readonly IMapper mapper;
        private readonly ISubjectsDbContext context;

        public GetListOfSubjectTasksHandler(IMapper mapper, ISubjectsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<SubjectTaskItemDTO>> Handle(GetListOfSubjectTasksQuery request, CancellationToken cancellationToken)
        {
            await CheckAccess(request);
            return await context.Tasks
                .AsNoTracking()
                .Where(t => t.Subject.ID == request.SubjectID && t.User.ID == request.UserID)
                .Select(t => mapper.Map<SubjectTaskItemDTO>(t))
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private async Task CheckAccess(GetListOfSubjectTasksQuery req)
        {
            Subject sub = await context.Subjects
                .AsNoTracking()
                .Include(s => s.Creator)
                .FirstOrDefaultAsync(s => s.ID  == req.SubjectID);

            if(sub == null)
            {
                throw new ResourceNotFoundException("Subject", req.SubjectID);
            }
            else if(!sub.Public && sub.Creator.ID != req.UserID)
            {
                throw new UnauthorizedResourceAccessException("Subject", req.SubjectID);
            }
        }
    }
}
