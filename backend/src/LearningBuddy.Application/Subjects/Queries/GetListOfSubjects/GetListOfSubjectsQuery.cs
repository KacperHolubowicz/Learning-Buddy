using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfSubjects
{
    public record GetListOfSubjectsQuery : IQuery<PaginatedList<SubjectItemDTO>>
    {
        public long? UserID { get; set; }
        public string Name { get; init; } = "";
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
                .Include(s => s.Creator)
                .Include(s => s.Tags)
                .AsNoTracking()
                .Where(s => s.Public 
                    && s.Name.ToLower().Contains(request.Name.ToLower()))
                .Select(s => new SubjectItemDTO()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Tags = s.Tags.Select(t => t.Value),
                    Thumbnail = s.Thumbnail,
                    Finished = s.Finished,
                    IsOwner = s.Creator.ID == request.UserID
                })
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
