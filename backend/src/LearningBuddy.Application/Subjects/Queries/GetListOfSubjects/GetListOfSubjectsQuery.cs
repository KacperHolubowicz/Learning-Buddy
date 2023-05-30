using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfSubjects
{
    public record GetListOfSubjectsQuery : IQuery<PaginatedList<SubjectItemDTO>>
    {
        public IEnumerable<string> Tags { get; init; } = Enumerable.Empty<string>();
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetListOfSubjectsHandler : IQueryHandler
        <GetListOfSubjectsQuery, PaginatedList<SubjectItemDTO>>
    {
        private readonly IMapper mapper;
        private readonly ISubjectsDbContext context;

        public GetListOfSubjectsHandler(IMapper mapper, ISubjectsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<SubjectItemDTO>> Handle(GetListOfSubjectsQuery request, CancellationToken cancellationToken)
        {
            return await context.Subjects
                .Include(s => s.Tags)
                .AsNoTracking()
                .Where(s => s.Public)
                .Select(s => mapper.Map<Subject, SubjectItemDTO>(s))
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
