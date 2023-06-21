using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetLearningSource
{
    public record GetLearningSourceQuery : IQuery<LearningSourceDTO>
    {
        public long UserID { get; set; }
        public long LearningSourceID { get; set; }
    }

    public class GetLearningSourceQueryHandler : IQueryHandler<GetLearningSourceQuery, LearningSourceDTO> 
    {
        private readonly ISubjectsDbContext sContext;
        private readonly IMapper mapper;

        public GetLearningSourceQueryHandler(ISubjectsDbContext sContext, IMapper mapper)
        {
            this.sContext = sContext;
            this.mapper = mapper;
        }

        public async Task<LearningSourceDTO> Handle(GetLearningSourceQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<LearningSourceDTO>
                (await FindLearningSource(request.UserID, request.LearningSourceID));
        }

        private async Task<LearningSource> FindLearningSource(long userId, long sourceId)
        {
            LearningSource source = await sContext.Sources
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.ID == sourceId);
            if(source == null)
            {
                throw new ResourceNotFoundException("LearningSource", sourceId);
            } else if(!source.Public && source.User.ID != userId)
            {
                throw new UnauthorizedResourceAccessException("LearningSource", sourceId);
            }
            return source;
        }
    }
}
