using AutoMapper;
using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfAttempts
{
    public record GetListOfAttemptsQuery : IQuery<PaginatedList<AttemptItemDTO>>
    {
        public long UserID { get; set; }
        public long QuizID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetListOfAttemptsQueryHandler : 
        IQueryHandler<GetListOfAttemptsQuery, PaginatedList<AttemptItemDTO>>
    {
        private readonly IMapper mapper;
        private readonly IQuizzesDbContext qContext;

        public GetListOfAttemptsQueryHandler(IMapper mapper, IQuizzesDbContext qContext)
        {
            this.mapper = mapper;
            this.qContext = qContext;
        }

        public async Task<PaginatedList<AttemptItemDTO>> Handle
            (GetListOfAttemptsQuery request, CancellationToken cancellationToken)
        {
            if(await CheckUsersAccessToSubject(request.UserID, request.QuizID))
            {
                return await qContext.Attempts
                    .Include(a => a.User)
                    .Include(a => a.Quiz)
                    .Where(a => a.Quiz.ID == request.QuizID && a.User.ID == request.UserID)
                    .Select(a => mapper.Map<AttemptItemDTO>(a))
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
            throw new ResourceNotFoundException("Quiz", request.QuizID);
        }

        private async Task<bool> CheckUsersAccessToSubject(long userID, long quizID)
        {
            Subject sub = (await qContext.Quizzes
                .Include(q => q.Subject)
                .ThenInclude(s => s.Creator)
                .FirstOrDefaultAsync(q => q.ID == quizID))
                .Subject;

            if (sub != null && (sub.Public || sub.Creator.ID == userID))
            {
                return true;
            }
            return false;
        }
    }
}
