using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using LearningBuddy.Domain.Subjects.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Queries.GetAttempt
{
    public class GetAttemptQuery : IQuery<AttemptDTO>
    {
        public long UserID { get; set; }
        public long AttemptID { get; set; }
    }

    public class GetAttemptQueryHandler : IQueryHandler<GetAttemptQuery, AttemptDTO>
    {
        private readonly IMapper mapper;
        private readonly IQuizzesDbContext qContext;

        public GetAttemptQueryHandler(IMapper mapper, IQuizzesDbContext qContext)
        {
            this.mapper = mapper;
            this.qContext = qContext;
        }

        public async Task<AttemptDTO> Handle(GetAttemptQuery request, CancellationToken cancellationToken)
        {
            await CheckUsersAccessToAttempt(request.UserID, request.AttemptID);
            return mapper.Map<AttemptDTO>(await qContext.Attempts
                .Include(a => a.Answers)
                .ThenInclude(a => a.Question)
                .Include(a => a.Answers)
                .ThenInclude(a => a.Answer)
                .FirstOrDefaultAsync(a => a.ID == request.AttemptID));
        }

        private async Task CheckUsersAccessToAttempt(long userID, long attemptID)
        {
            Attempt att = await qContext.Attempts
                .Include(a => a.User)
                .Include(a => a.Quiz)
                .ThenInclude(q => q.Subject)
                .ThenInclude(s => s.Creator)
                .FirstOrDefaultAsync(a => a.ID == attemptID);
            if(att == null)
            {
                throw new ResourceNotFoundException("Attempt", attemptID);
            }

            Subject sub = att.Quiz.Subject;
            if ((!sub.Public && sub.Creator.ID != userID) || att.User.ID != userID)
            {
                throw new UnauthorizedResourceAccessException("Attempt", attemptID);
            }
        }
    }
}
