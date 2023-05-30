using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Subjects.Queries.GetSubject
{
    public record GetSubjectQuery : IQuery<SubjectDTO>
    {
        public long? UserID { get; set; }
        public long SubjectID { get; set; }
    }

    public class GetSubjectQueryHandler : IQueryHandler<GetSubjectQuery, SubjectDTO>
    {
        private readonly IMapper mapper;
        private readonly ISubjectsDbContext context;

        public GetSubjectQueryHandler(IMapper mapper, ISubjectsDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<SubjectDTO> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Subjects
                .Include(s => s.Creator)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ID == request.SubjectID
                    && ((request.UserID.HasValue && s.Creator.ID == request.UserID) || s.Public == true));

            if(entity == null)
            {
                throw new ResourceNotFoundException("Subject", request.SubjectID);
            }
            return mapper.Map<SubjectDTO>(entity);
        }
    }
}
