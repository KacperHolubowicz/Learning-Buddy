using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources
{
    public record GetListOfLearningSourcesQuery : IQuery<PaginatedList<LearningSourceDTO>>
    {
        public long? UserID { get; set; }
        public long SubjectID { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetListOfLearningSourcesHandler : IQueryHandler<GetListOfLearningSourcesQuery, 
        PaginatedList<LearningSourceDTO>>
    {
        private readonly IMapper mapper;
        private readonly ISubjectsDbContext context;

        public GetListOfLearningSourcesHandler(IMapper mapper, ISubjectsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<LearningSourceDTO>> Handle(GetListOfLearningSourcesQuery request, CancellationToken cancellationToken)
        {
            return await context.Sources
                .Include(s => s.User)
                .AsNoTracking()
                .Where(s => s.Subject.ID == request.SubjectID && s.Public)
                .Select(s => new LearningSourceDTO()
                {
                    Description = s.Description,
                    Name = s.Name,
                    Type = s.Type.ToString(),
                    IsOwner = s.User.ID == request.UserID
                })
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
