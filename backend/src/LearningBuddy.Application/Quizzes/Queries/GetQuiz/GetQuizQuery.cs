using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
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
            await CheckUsersAccessToSubject(request.UserID, request.QuizID);
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

        private async Task CheckUsersAccessToSubject(long? userID, long quizID)
        {
            Quiz quiz = await qContext.Quizzes
                .Include(q => q.Subject)
                .ThenInclude(s => s.Creator)
                .FirstOrDefaultAsync(q => q.ID == quizID);
            
            if(quiz == null)
            {
                throw new ResourceNotFoundException("Quiz", quizID);
            }
            else if(!quiz.Subject.Public && quiz.Subject.Creator.ID != userID)
            {
                throw new UnauthorizedResourceAccessException("Quiz", quizID);
            }
        }
    }
}
