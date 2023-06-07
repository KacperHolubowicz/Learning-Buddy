using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
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
            await CheckAccess(request);
            return await context.Sources
                .AsNoTracking()
                .Where(s => s.User.ID == request.UserID
                    && s.Subject.ID == request.SubjectID)
                .Select(s => mapper.Map<PrivateLearningSourceDTO>(s))
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private async Task CheckAccess(GetListOfPrivateLearningSourcesQuery req)
        {
            Subject sub = await context.Subjects
                .AsNoTracking()
                .Include(s => s.Creator)
                .FirstOrDefaultAsync(s => s.ID == req.SubjectID);

            if (sub == null)
            {
                throw new ResourceNotFoundException("Subject", req.SubjectID);
            }
            else if (!sub.Public && sub.Creator.ID != req.UserID)
            {
                throw new UnauthorizedResourceAccessException("Subject", req.SubjectID);
            }
        }
    }
}
