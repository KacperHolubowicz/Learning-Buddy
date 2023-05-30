using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfPrivateSubjects
{
    public record GetListOfPrivateSubjectsQuery : IQuery<PaginatedList<PrivateSubjectItemDTO>>
    {
        public long UserID { get; set; }
        public IEnumerable<string> Tags { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetListOfPrivateSubjectsHandler : IQueryHandler
        <GetListOfPrivateSubjectsQuery, PaginatedList<PrivateSubjectItemDTO>>
    {
        private readonly IMapper mapper;
        private readonly ISubjectsDbContext context;

        public GetListOfPrivateSubjectsHandler(IMapper mapper, ISubjectsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<PrivateSubjectItemDTO>> Handle(GetListOfPrivateSubjectsQuery request, CancellationToken cancellationToken)
        {
            return await context.Subjects
                .Include(s => s.Tags)
                .AsNoTracking()
                .Where(s => s.Creator.ID == request.UserID)
                .Select(s => mapper.Map<Subject, PrivateSubjectItemDTO>(s))
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
