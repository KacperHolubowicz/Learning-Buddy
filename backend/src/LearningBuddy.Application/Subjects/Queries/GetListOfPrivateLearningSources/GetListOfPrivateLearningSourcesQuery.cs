using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetListOfPrivateLearningSources
{
    public record GetListOfPrivateLearningSourcesQuery : IQuery<PaginatedList<PrivateLearningSourceDTO>>
    {
        public long UserID { get; set; }
        public long SubjectID { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetListOfPrivateLearningSourcesHandler : IQueryHandler<GetListOfPrivateLearningSourcesQuery,
        PaginatedList<PrivateLearningSourceDTO>>
    {
        private readonly IMapper mapper;
        private readonly ISubjectsDbContext context;

        public GetListOfPrivateLearningSourcesHandler(IMapper mapper, ISubjectsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<PrivateLearningSourceDTO>> Handle(GetListOfPrivateLearningSourcesQuery request, CancellationToken cancellationToken)
        {
            return await context.Sources
                .AsNoTracking()
                .Where(s => s.User.ID == request.UserID
                    && s.Subject.ID == request.SubjectID)
                .Select(s => mapper.Map<PrivateLearningSourceDTO>(s))
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
