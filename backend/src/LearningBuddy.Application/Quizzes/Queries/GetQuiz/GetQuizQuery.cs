using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Queries.GetQuiz
{
    public record GetQuizQuery : IQuery<QuizDTO>
    {
        public long? UserID { get; set; }
        public long QuizID { get; set; }
    }

    public class GetQuizQueryHandler : IQueryHandler<GetQuizQuery, QuizDTO>
    {
        private readonly IMapper mapper;
        private readonly IQuizzesDbContext qContext;

        public GetQuizQueryHandler(IMapper mapper, IQuizzesDbContext qContext)
        {
            this.mapper = mapper;
            this.qContext = qContext;
        }

        public async Task<QuizDTO> Handle(GetQuizQuery request, CancellationToken cancellationToken)
        {
            if(await CheckUsersAccessToSubject(request.UserID, request.QuizID))
            {
                var quizEntity = await qContext.Quizzes
                    .Include(q => q.User)
                    .Include(q => q.Subject)
                    .Include(q => q.Questions)
                    .Include(q => q.Attempts)
                    .FirstOrDefaultAsync(q => q.ID == request.QuizID);
                var quizDto = mapper.Map<QuizDTO>(quizEntity);
                quizDto.IsOwner = quizEntity.User.ID == request.UserID;
                return quizDto;
            }
            throw new ResourceNotFoundException("Quiz", request.QuizID);
        }

        private async Task<bool> CheckUsersAccessToSubject(long? userID, long quizID)
        {
            Subject sub = (await qContext.Quizzes
                .Include(q => q.Subject)
                .ThenInclude(s => s.Creator)
                .FirstOrDefaultAsync(q => q.ID == quizID))
                .Subject;

            if (sub != null && (sub.Public || userID.HasValue && sub.Creator.ID == userID))
            {
                return true;
            }
            return false;
        }
    }
}
