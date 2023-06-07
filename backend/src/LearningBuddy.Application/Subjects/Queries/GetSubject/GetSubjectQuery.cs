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
            var subject = await context.Subjects
                .Include(s => s.Creator)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ID == request.SubjectID);

            if(subject == null)
            {
                throw new ResourceNotFoundException("Subject", request.SubjectID);
            }
            else if(!subject.Public && subject.Creator.ID != request.UserID)
            {
                throw new UnauthorizedResourceAccessException("Subject", request.SubjectID);
            } 
            var subjectDto = mapper.Map<SubjectDTO>(subject);
            subjectDto.IsOwner = subject.Creator.ID == request.UserID;
            return subjectDto;
        }
    }
}
